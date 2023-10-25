using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Rotifer : MonoBehaviour
{
    public Animator rotiferAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Volvox volvox = other.GetComponent<Volvox>();
        if (volvox)
        {
            volvox.RemoveColony();
            Debug.Log("1 colony is eaten by rotifer");
        }
    }
}
