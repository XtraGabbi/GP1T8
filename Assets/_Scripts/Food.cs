using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] float healthIncrease;

    // Get eaten by volvox when volvox is in trigger
    private void OnTriggerEnter(Collider collition)
    {
        if (collition.GetComponent<VolvoxHealth>())
        {
            VolvoxHealth volvoxHealth = collition.GetComponent<VolvoxHealth>();
            volvoxHealth.health += healthIncrease;

            Volvox.Instance.AddColony();

            Debug.Log("Add colony");

            Destroy(gameObject);
        }
    }
}