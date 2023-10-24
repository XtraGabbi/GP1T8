using UnityEngine;
using UnityEngine.Events;

public class GameTimer : MonoBehaviour
{
    [Header("Game Time (Seconds)")]
    public float lifeTimer;
    [Header("Event")]
    [SerializeField] UnityEvent onTimerEnd;

    private void FixedUpdate()
    {            
            lifeTimer -= Time.deltaTime;
            LifeChecker();
    }
    
    void LifeChecker()
    {
        if (lifeTimer <= 0)
        {
            Debug.Log("Volvox lifespan ended. Aka Game Over");
            onTimerEnd.Invoke();
        }
    }
}
