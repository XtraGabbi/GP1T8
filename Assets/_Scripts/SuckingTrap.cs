using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SuckingTrap : MonoBehaviour
{
    private Volvox _volvox;
    public bool DrawGizmos;
    public float radius = 1f;
    public float suckStrength;
    private float distToVolvox;

    // Start is called before the first frame update
    void Start()
    {
        _volvox = Volvox.Instance;

    }

    void OnDrawGizmos()
    {
        if (DrawGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, radius);
        }
    }

    // Update is called once per frame
    void Update()
    {
        distToVolvox = (transform.position - _volvox.transform.position).magnitude;

        
    }

    private void FixedUpdate()
    {
        if (distToVolvox < radius)
        {
            _volvox.rb.AddForce(suckStrength * (transform.position - _volvox.transform.position) / (Mathf.Pow(distToVolvox, 3) + 0.01f));
        }
    }
}