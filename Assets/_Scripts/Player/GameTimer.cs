using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GameTimer : MonoBehaviour
{
    #region Config
    [Header("GameTime Settings")]
    public float lifeTimer;
    [SerializeField] Color targetColor;
    [SerializeField] Color targetEmissionColor;
    
    [Header("Event")]
    [SerializeField] UnityEvent onTimerEnd;
    
    Renderer _materialToChange;
    float _maxLifeTimer;
    Color _initialColor;
    Color _initialEmissionColor;
    #endregion

    private void Awake()
    {
        _initialEmissionColor = _materialToChange.material.GetColor("_EmissionColor");
        _initialColor = _materialToChange.material.color;
        _maxLifeTimer = lifeTimer;
    }

    private void FixedUpdate()
    {            
            lifeTimer -= Time.fixedDeltaTime;
            LifeChecker();
            ColorTransition();
    }
    
    void LifeChecker()
    {
        if (lifeTimer <= 0)
        {
            Debug.Log("Volvox lifespan ended. Aka Game Over");
            onTimerEnd.Invoke();
        }
    }
    void ColorTransition()
    {
        float lerpValue = 1 - (lifeTimer / _maxLifeTimer);
        Color newEmissionColor = Color.Lerp(_initialEmissionColor, targetEmissionColor, lerpValue);
        _materialToChange.material.color = Color.Lerp(_initialColor, targetColor, lerpValue);
        Debug.Log("Lerp value: " + lerpValue);
        _materialToChange.material.SetColor("_EmissionColor", newEmissionColor);
    }
}
