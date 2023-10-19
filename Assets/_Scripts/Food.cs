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
        if(collition.GetComponent<VolvoxHealth>())
        {
            VolvoxHealth volvox = collition.GetComponent<VolvoxHealth>();
            volvox.health += healthIncrease;
            Destroy(gameObject);
        }
    }
}
