using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Volvox : MonoBehaviour
{
    public static Volvox instance;

    [Header("Lerp with self position")] public GameObject lerpTarget;
    public float targetlerpSpeed = 0.1f;
    public float thislerpSpeed = 0.1f;

    [Header("For PID")] public Rigidbody rb;

    public float controlParam_Proportion = 0.01f;
    public float controlParam_Differentiation = 0.42f;

    private Vector3 error = Vector3.zero;
    private Vector3 errorLastTime = Vector3.zero;

    private LightManager _lightManager;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _lightManager = LightManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (_lightManager.LightSourceLeft.isOn && _lightManager.LightSourceRight.isOn)
        {
        }
        else if (_lightManager.LightSourceLeft.isOn || _lightManager.LightSourceRight.isOn)
        {
            lerpTarget.transform.position = Vector3.Lerp(lerpTarget.transform.position,
                LightManager.instance.ShiningLight.transform.position, targetlerpSpeed * Time.deltaTime);
        }
        else
        {
        }

        transform.position = Vector3.Lerp(transform.position,
            lerpTarget.transform.position, thislerpSpeed * Time.deltaTime);
    }

    // private void FixedUpdate()
    // {
    //     PIDControl(null,null);
    // }


    void movingTowardsLight()
    {
        LerpPosition();
    }

    void PIDControl(Transform current, Transform target)
    {
        LightSource shiningLight = LightManager.instance.ShiningLight;

        error = shiningLight.transform.position - transform.position;
        Vector3 force = controlParam_Proportion * error +
                        controlParam_Differentiation * (error - errorLastTime) / Time.fixedDeltaTime;
        rb.AddForce(force);

        errorLastTime = error;
    }

    void LerpPosition()
    {
        lerpTarget.transform.position = Vector3.Lerp(lerpTarget.transform.position,
            LightManager.instance.ShiningLight.transform.position, targetlerpSpeed * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position,
            lerpTarget.transform.position, thislerpSpeed * Time.deltaTime);
    }
}