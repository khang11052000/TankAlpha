using System;
using System.Collections;
using System.Collections.Generic;
using Health;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        Destroy(gameObject);

        TankMovement tankMovement = other.gameObject.GetComponent<TankMovement>();
        tankMovement.SetSpeed(tankMovement.GetSpeed() * 2f);

        // TankHealth tankHealth = other.gameObject.GetComponent<TankHealth>();
        // tankHealth.TakeDamage(60f);
        
        Debug.Log(other.gameObject);
       
    }
    
}
