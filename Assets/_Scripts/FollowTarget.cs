using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public static FollowTarget Instance;
    // Start is called before the first frame update

    public float targetlerpSpeed = 0.1f;
    private LightManager _lightManager;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        _lightManager = LightManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (_lightManager.LightSourceLeft.isOn && _lightManager.LightSourceRight.isOn)
        {
            Vector3 leftLightPos = _lightManager.LightSourceLeft.transform.position;
            Vector3 rightLightPos = _lightManager.LightSourceRight.transform.position;
            Vector3 dirBetweenLight = (rightLightPos - leftLightPos).normalized;
            Vector3 nearestPoint = Vector3.Dot(transform.position - leftLightPos, dirBetweenLight) * dirBetweenLight +
                                   leftLightPos;
            
            transform.position = Vector3.Lerp(transform.position,nearestPoint, targetlerpSpeed * Time.deltaTime);
        }
        else if (_lightManager.LightSourceLeft.isOn || _lightManager.LightSourceRight.isOn)
        {
            LightSource light = _lightManager.LightSourceLeft.isOn
                ? _lightManager.LightSourceLeft
                : _lightManager.LightSourceRight;
            
            transform.position = Vector3.Lerp(transform.position,
                light.transform.position, targetlerpSpeed * Time.deltaTime);
        }
        else
        {
        }
    }
}