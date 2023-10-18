using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class LightManager : MonoBehaviour
{
    public static LightManager instance;

    public LightSource LightSource1;
    public LightSource LightSource2;

    public LightSource ShiningLight;

    public InputAction switchLightAction;


    private GameObject Algea;

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

        switchLightAction.started += ctx =>
        {
            print("SwitchLight!");
            SwitchLight();
        };
    }

    private void OnEnable()
    {
        switchLightAction.Enable();
    }
    private void OnDisable()
    {
        switchLightAction.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        Algea = GameObject.FindWithTag("Algae");
        LightSource1.turnOnTheLight();
        LightSource2.turnOffTheLight();
    }

    // Update is called once per frame
    void Update()
    {

        ShiningLight.shootLightTowardsTargetPosition(Algea.transform.position);
    }

    public void SwitchLight()
    {
        LightSource1.Toggle();
        LightSource2.Toggle();
    }
}
