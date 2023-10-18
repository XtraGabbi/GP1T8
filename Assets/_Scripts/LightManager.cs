using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Serialization;

public class LightManager : MonoBehaviour
{
    public static LightManager Instance;

    public LightSource LightSourceLeft;
    public LightSource LightSourceRight;

    public InputAction toggleLeftLightAction;

    public InputAction toggleRightLightAction;

    private Volvox _volvox;

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

        toggleLeftLightAction.started += ctx => { LightSourceLeft.Toggle(); };

        toggleRightLightAction.started += ctx => { LightSourceRight.Toggle(); };
    }

    private void OnEnable()
    {
        toggleLeftLightAction.Enable();
        toggleRightLightAction.Enable();
    }

    private void OnDisable()
    {
        toggleLeftLightAction.Disable();
        toggleRightLightAction.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        _volvox = Volvox.Instance;
        LightSourceLeft.turnOnTheLight();
        LightSourceRight.turnOffTheLight();
    }

    // Update is called once per frame
    void Update()
    {
        if (LightSourceLeft.isOn)
        {
            LightSourceLeft.shootLightTowardsTargetPosition(_volvox.transform.position);
        }

        if (LightSourceRight.isOn)
        {
            LightSourceRight.shootLightTowardsTargetPosition(_volvox.transform.position);
        }
    }

    public void SwitchLight()
    {
        LightSourceLeft.Toggle();
        LightSourceRight.Toggle();
    }
}