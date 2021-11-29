using Health;
using UnityEngine;

namespace Item
{
    public class ItemHealth : Item
    {
        private void OnTriggerEnter(Collider other)
        {
            TankHealth tankHealth = other.gameObject.GetComponent<TankHealth>();
            tankHealth.TakeDamage(-40f);
        
            Destroy(gameObject);
        
            Debug.Log(other.gameObject);
        }
    }
}
