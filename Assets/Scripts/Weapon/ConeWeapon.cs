using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(menuName = "Weapon/Cone")]
    public class ConeWeapon : BaseWeapon
    {
        public override void Fire(Transform firePoint, float force)
        {
            for (int i = -2; i <= 2; i++)
            {
                Vector3 bulletPosition = firePoint.position;
                Vector3 bulletRotation = firePoint.rotation.eulerAngles;
                bulletRotation.y += 20f * i;
                
                // Create an instance of the shell and store a reference to it's rigidbody.
                Rigidbody shellInstance = Instantiate(shell, bulletPosition, Quaternion.Euler(bulletRotation)) as Rigidbody;

                // Set the shell's velocity to the launch force in the fire position's forward direction.
                shellInstance.velocity = force * shellInstance.transform.forward;
            }
        }
    }
}