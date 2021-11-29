using System;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Tank
{
    public class TankBrain : MonoBehaviour
    {
        public NavMeshAgent navMeshAgent;
        public float scanningRadius = 5f;
        
        public float patrollingSpeed = 1f;
        public float chasingSpeed = 5f;
        
        public float attackSpeed = 1f;
        public int attackDamage = 1;
        public Transform attackPoint;
        public float attackRange = 0.5f;
        public float attackForce = 5f;
        public GameObject attackBullet;

        [SerializeField] private BehaviorTree bTree;

        private bool _foundTarget;
        private Transform _target;
        private GameObject _targetEntity;
        
        private Vector3 _originPoint;
        private Vector3 _destPoint;

        private float _attackInterval;

        private void OnValidate()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Awake()
        {
            bTree = new BehaviorTreeBuilder(gameObject)
                .Selector()
                    .Sequence()
                        .Condition("Close enough to attack?", ShouldAttack)
                        .Do("Attack", Attack)
                    .End()
                
                    .Sequence()
                        .Condition("Lost or killed player?", () => !_foundTarget)
                        .Do("Patrol", Patrol)
                    .End()
                    
                    .Sequence()
                        .Condition("Player on sight?", () => _foundTarget)
                        .Do("Chase", Chase)
                    .End()
                .End()
                .Build();
        }

        private void Start()
        {
            navMeshAgent.autoBraking = false;
            // navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;

            _originPoint = transform.position;
            
            GotoNextPoint();
        }

        private void Update()
        {
            bTree.Tick();
        }

        private void FixedUpdate()
        {
            FindingTarget();
        }

        private bool ShouldAttack()
        {
            if (!_foundTarget)
                return false;

            return true;
            
            float distance = Vector3.Distance(transform.position, _target.position);
            // Debug.Log(distance);
            return distance <= 15f;
        }

        private TaskStatus Attack()
        {
            if (_attackInterval > 0f)
            {
                _attackInterval -= Time.deltaTime;
                return TaskStatus.Continue;
            }
            
            _attackInterval = attackSpeed;
            
            Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, attackRange, LayerMask.GetMask("Players"));
            
            for (var i = 0; i < hitColliders.Length; i++)
            {
                // Logic attack player right here
                // _targetEntity.OnTakeDamage?.Invoke(attackDamage);
                Fire();
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }

        private TaskStatus Patrol()
        {
            navMeshAgent.speed = patrollingSpeed;
            
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                GotoNextPoint();
            
            return TaskStatus.Success;
        }
        
        private TaskStatus Chase()
        {
            navMeshAgent.speed = chasingSpeed;
            navMeshAgent.SetDestination(_target.position);
            return TaskStatus.Success;
        }
 
        private void FindingTarget()
        {
            if (_foundTarget)
                return;

            Collider[] collisions =
                Physics.OverlapSphere(transform.position, scanningRadius, LayerMask.GetMask("Players"));
            
            
            for (var i = 0; i < collisions.Length; i++)
            {
                _foundTarget = true;
                _target = collisions[i].transform;
                // _targetEntity = collision.gameObject.GetComponent<BaseEntity>();
                // _targetEntity.OnDeath += ResetTarget;
                break;
            }
        }

        private void ResetTarget()
        {
            _foundTarget = false;
            _target = null;
            _targetEntity = null;
            
            // Move to origin position
            navMeshAgent.destination = _originPoint;
        }
        
        private void GotoNextPoint()
        {
            var position = transform.position;
            float x = Random.Range(position.x - scanningRadius, position.x + scanningRadius);
            float z = Random.Range(position.z - scanningRadius, position.z + scanningRadius);
            
            _destPoint = new Vector3(x, 0, z);

            NavMeshPath path = new NavMeshPath();
            navMeshAgent.CalculatePath(_destPoint, path);
            if (path.status == NavMeshPathStatus.PathPartial)
            {
                GotoNextPoint();
                return;
            }
            
            navMeshAgent.destination = _destPoint;
        }

        private void Fire()
        {
            // Create an instance of the shell and store a reference to it's rigidbody.
            var bullet = Instantiate(attackBullet, attackPoint.position, attackPoint.rotation);

            var rig = bullet.GetComponent<Rigidbody>();

            // Set the shell's velocity to the launch force in the fire position's forward direction.
            rig.velocity = attackForce * rig.transform.forward;
        }
    }
}
