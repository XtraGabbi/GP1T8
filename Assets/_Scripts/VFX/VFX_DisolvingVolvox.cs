using System;
using UnityEngine;

public class VFX_DisolvingVolvox : MonoBehaviour
{
    [SerializeField] float timeItsDisolving;
    [SerializeField] Material disolvingMaterial;
    [SerializeField] Renderer targetObject; // Assign the Renderer of the object you want to change the material for

    float _timer;
    private bool _isDisolving = false; // to control the disolving process
    

    private void Awake()
    {
        
    }

    void Update()
    {
        if (_isDisolving)
        {
            _timer -= Time.deltaTime;
            Disolve();
        }
    }

    public void AddDisolveEffect()
    {
        targetObject.material = disolvingMaterial; // This changes the material of the target object to the dissolving material
        _isDisolving = true;
        _timer = timeItsDisolving; // Initialize timer when starting the dissolve
        Debug.Log("adding effect");
    }
    
    void Disolve()
    {
        targetObject.material.SetFloat("_CutoffHeight", _timer);
        Debug.Log("disolving");
    }
}