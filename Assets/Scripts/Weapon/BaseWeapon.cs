using UnityEngine;

namespace Weapon
{
    public abstract class BaseWeapon : ScriptableObject
    {
        public Rigidbody shell;
        public abstract void Fire(Transform firePoint, float force);
    }
}