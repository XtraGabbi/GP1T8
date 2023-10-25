using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Foodv2 : MonoBehaviour
{
    private FoodSpawnSystem spawnSystem;

    
    private void Start()
    {
        spawnSystem = GameObject.FindObjectOfType<FoodSpawnSystem>();
    }
    

    // Get eaten by volvox when volvox is in trigger
    private void OnTriggerEnter(Collider collition)
    {
        if (collition.GetComponent<Volvox>())
        {
            Volvox.Instance.AddColony();

            Debug.Log("Add colony");

            if (spawnSystem != null)
            {
                Debug.Log("spawnsystem != null");
                spawnSystem.FoodDestroyed(gameObject);
            }

            Destroy(gameObject);


        }
    }
}
