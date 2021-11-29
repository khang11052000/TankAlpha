using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Item
{
    public class ItemManager : MonoBehaviour
    {

        public List<GameObject> items;
        public List<Transform> spawnPoints;
        
        private void Awake()
        {
            // Khowri 
            foreach (var spawnPoint in spawnPoints)
            {
                Instantiate(items[Random.Range(0, items.Count)], spawnPoint);
            }
        }
    }
}