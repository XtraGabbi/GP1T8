using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class SuckingTrap : MonoBehaviour
{
    private Volvox _volvox;
    private Rigidbody _volvoxRb;
    public bool DrawGizmos;
    public float outerRange = 5f;
    public float innerRange = 3f;
    public float spinStrength = 10f;
    private float distToVolvox;
    private Vector3 _centripetalVector;
    public float desiredDist;
    private float _dist;
    private Vector3 centripetalDir;

    private Vector3 tangentDir;

    // Start is called before the first frame update
    void Start()
    {
        _volvox = Volvox.Instance;
        _volvoxRb = _volvox.rb;
    }

    void OnDrawGizmos()
    {
        if (DrawGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, outerRange);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _centripetalVector = transform.position - _volvox.transform.position;
        _dist = _centripetalVector.magnitude;
    }

    private void FixedUpdate()
    {
        if (_dist < outerRange)
        {
            centripetalDir = _centripetalVector.normalized;

            // spin counter clockwise;
            if (Vector3.Dot(Vector3.up, Vector3.Cross(centripetalDir, _volvoxRb.velocity)) > 0)
            {
                tangentDir = Vector3.Cross(Vector3.up, centripetalDir);
            }
            else
            {
                tangentDir = Vector3.Cross(Vector3.down, centripetalDir);
            }

            Vector3 tangentAccel = tangentDir * spinStrength;
            _volvoxRb.AddForce(tangentAccel, ForceMode.Acceleration);

            Vector3 centripetalAccel = centripetalDir * Mathf.Pow(_volvoxRb.velocity.magnitude, 2) / desiredDist;
            _volvoxRb.AddForce(centripetalAccel, ForceMode.Acceleration);

            if (desiredDist > innerRange)
            {
                desiredDist *= 0.99f;
            }
        }
        else
        {
            desiredDist = outerRange;
        }
    }
}