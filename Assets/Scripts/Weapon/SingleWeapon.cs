using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(menuName = "Weapon/Single")]
    public class SingleWeapon : BaseWeapon
    {
        public override void Fire(Transform firePoint, float force)
        {
            // Create an instance of the shell and store a reference to it's rigidbody.
            Rigidbody shellInstance = Instantiate(shell, firePoint);

            // Set the shell's velocity to the launch force in the fire position's forward direction.
            shellInstance.velocity = force * shellInstance.transform.forward;
        }
    }
}