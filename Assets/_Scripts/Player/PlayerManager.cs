using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset actions;

    [SerializeField] private GameObject lightLeft;
    [SerializeField] private GameObject lightRight;
    [SerializeField] private GameObject movingObject;
    
    [Header("Player Settings")]
    [SerializeField] private float speed;
    
    private InputAction _lightRight;
    private InputAction _lightLeft;
    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    private void Awake()
    {
        actions.FindAction("LightRight").performed += ctx => LightRight();
        actions.FindAction("LightLeft").performed += ctx => LightLeft();
    }

    private void FixedUpdate()
    {
        if (lightRight.activeSelf && lightLeft.activeSelf)
        {
            Debug.Log("do nothing");
        }
        else if (lightRight.activeSelf)
        {
            movingObject.transform.position = Vector3.MoveTowards(movingObject.transform.position, lightRight.transform.position, speed*Time.fixedDeltaTime);
            Debug.Log("Moving Right");
        }
        else if (lightLeft.activeSelf)
        {
            movingObject.transform.position = Vector3.MoveTowards(movingObject.transform.position, lightLeft.transform.position, speed*Time.fixedDeltaTime);
            Debug.Log("Moving Left");
        }   
    }

    private void LightRight()
    {
        Debug.Log("Light Right");
        if (lightRight.activeSelf)
        {
            lightRight.SetActive(false);
        }
        else
        {
            lightRight.SetActive(true);
        }
        
    }
    
    private void LightLeft()
    {
        Debug.Log("Light Left");
        if (lightLeft.activeSelf)
        {
            lightLeft.SetActive(false);
        }
        else
        {
            lightLeft.SetActive(true);
        }
    }

}
