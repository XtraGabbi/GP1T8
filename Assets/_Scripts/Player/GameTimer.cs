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
    [SerializeField] Renderer materialToChange;

    [Header("Event")]
    public UnityEvent onTimerEnd;
    
    Renderer _materialToChange;
    float _maxLifeTimer;
    Color _initialColor;
    Color _initialEmissionColor;
    #endregion

    private void Awake()
    {
        _initialEmissionColor = materialToChange.material.GetColor("_EmissionColor");
        _initialColor = materialToChange.material.color;
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
            this.enabled = false;
        }
    }
    void ColorTransition()
    {
        float lerpValue = 1 - (lifeTimer / _maxLifeTimer);
        Color newEmissionColor = Color.Lerp(_initialEmissionColor, targetEmissionColor, lerpValue);
        materialToChange.material.color = Color.Lerp(_initialColor, targetColor, lerpValue);
        // Debug.Log("Lerp value: " + lerpValue);
        materialToChange.material.SetColor("_EmissionColor", newEmissionColor);
    }
}
