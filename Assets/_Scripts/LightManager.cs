using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Serialization;

public class LightManager : MonoBehaviour
{
    public static LightManager instance;

    public LightSource LightSourceLeft;
    public LightSource LightSourceRight;

    public LightSource ShiningLight;

    public InputAction toggleLeftLightAction;

    public InputAction toggleRightLightAction;

    private Volvox _volvox;

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

        toggleLeftLightAction.started += ctx =>
        {
            LightSourceLeft.Toggle();
        };
        
        toggleRightLightAction.started += ctx =>
        {
            LightSourceRight.Toggle();
        };
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
        _volvox = Volvox.instance;
        LightSourceLeft.turnOnTheLight();
        LightSourceRight.turnOffTheLight();
    }

    // Update is called once per frame
    void Update()
    {
        ShiningLight.shootLightTowardsTargetPosition(_volvox.transform.position);
    }

    public void SwitchLight()
    {
        LightSourceLeft.Toggle();
        LightSourceRight.Toggle();
    }
}
