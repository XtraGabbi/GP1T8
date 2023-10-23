using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Food : MonoBehaviour
{
    // Get eaten by volvox when volvox is in trigger
    private void OnTriggerEnter(Collider collition)
    {
        if (collition.GetComponent<VolvoxColonyCount>())
        {
            VolvoxColonyCount volvoxColonyCount = collition.GetComponent<VolvoxColonyCount>();
            volvoxColonyCount.colonyCount++;

            Volvox.Instance.AddColony();

            Debug.Log("Add colony");

            Destroy(gameObject);
        }
    }
}