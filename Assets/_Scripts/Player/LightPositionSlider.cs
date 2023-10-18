using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LightPositionSlider : MonoBehaviour
{
    [SerializeField] private float maxHight;
    [SerializeField] private float maxWidth;
    [SerializeField] private int placementRange;
    [SerializeField] Camera mainCamera;

    [Header("Pacement Settings")] 
    [SerializeField] private bool placeItLeft;
    [SerializeField] private bool placeItRight;
    private void Awake()
    {
        mainCamera = Camera.main;
        maxHight = Screen.height;
        maxWidth = Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        if (placeItLeft)
        {
            LightPlacement(maxHight, 0);
        }
        else if (placeItRight)
        {
            LightPlacement(maxHight, maxWidth);
        }
    }

    void LightPlacement(float hight, float width)
    {
        gameObject.transform.position = mainCamera.ScreenToWorldPoint(new Vector3(width, hight, placementRange));
    }
    public void UpdatePlacement()
    {
        maxHight = Screen.height;
        maxWidth = Screen.width;
    }
    
}
