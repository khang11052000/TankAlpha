using UnityEngine;

namespace Item
{
    public class ItemBullet : Item
    {
        private void OnTriggerEnter(Collider other)
        {
            TankShooting tankShooting = other.gameObject.GetComponent<TankShooting>();
        
        }
    }
}
