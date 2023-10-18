using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckingTrap : MonoBehaviour
{
    private Volvox _volvox;
    public bool DrawGizmos;
    public float radius = 1f;
    private float distToAlgae;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnDrawGizmos()
    {
        if (DrawGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(this.transform.position, radius);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // distToAlgae
    }
}