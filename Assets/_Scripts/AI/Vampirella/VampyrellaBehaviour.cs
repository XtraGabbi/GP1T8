using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class VampyrellaBehaviour : MonoBehaviour
{
    #region Config
    [Header("Vampyrella Settings")]
    [SerializeField] float movementSpeed;
    [SerializeField] float giveUpChaseTimer;
    [SerializeField] float attachedToPlayerTimer;
    [SerializeField] float attachDistance;
    [SerializeField] int damage;
    [Space(15)]
    
    [SerializeField] GameObject player;
    [SerializeField] VolvoxHealth volvoxHealth;
    Vector3 offset;
    float _timer;
    #endregion

    #region AI_States
    enum EnemyState
    {
        IdleForwards,
        Chasing,
        Attached,
        GoesAway
    }
    EnemyState _enemyState;
    #endregion
    void FixedUpdate()
    {
        AIVampyrella();
    }

    void AIVampyrella()
    {
        #region AI_Logic
        switch (_enemyState)
        {
            case EnemyState.IdleForwards:
                gameObject.transform.Translate(Vector3.forward * (Time.fixedDeltaTime * movementSpeed));
                break; // Moves in a straight line.
            case EnemyState.Chasing:
                gameObject.transform.LookAt(player.transform);
                gameObject.transform.Translate(Vector3.forward * (Time.fixedDeltaTime * movementSpeed));
                TimerCountdown();
                if (giveUpChaseTimer < _timer)
                {
                    Debug.Log("Give up");
                    TimerReset();
                    gameObject.transform.LookAt(player.transform.position * -1);
                    _enemyState = EnemyState.GoesAway;
                }
                else if (Vector3.Distance(gameObject.transform.position, player.transform.position) < 1f)
                {
                    Debug.Log("I'm attached");
                    TimerReset();
                    offset = transform.position - player.transform.position;
                    _enemyState = EnemyState.Attached;
                }
                break; // Moves towards the player. It will give up after a certain amount of time, based on [giveUpTimer].
            case EnemyState.Attached:
                Debug.Log("I'm feeding");
                TimerCountdown();
                KeepCurrentPositionWithObject(player.transform.position);
                if (attachedToPlayerTimer < _timer)
                {
                    volvoxHealth.health -= damage;
                    Debug.Log("I'm done feeding");
                    TimerReset();
                    gameObject.transform.LookAt(player.transform.position * -1);
                    _enemyState = EnemyState.GoesAway;
                }
                break; // Sticks to the player and drains health, based on [damage].
            case EnemyState.GoesAway:
                gameObject.transform.Translate(Vector3.forward * (Time.fixedDeltaTime * movementSpeed));
                break; // Moves in a straight line and fades away.
            default:
                throw new ArgumentOutOfRangeException();
        }
        #endregion
    }

    void TimerCountdown()
    {
        _timer += Time.fixedDeltaTime;
    }
    void TimerReset()
    {
        _timer = 0f;
    }
    void KeepCurrentPositionWithObject(Vector3 objectPosition)
    {
        transform.position = objectPosition + offset;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<VolvoxHealth>()) 
        {
            _enemyState = EnemyState.Chasing;
            player = collision.GetComponent<VolvoxHealth>().gameObject;
            volvoxHealth = collision.GetComponent<VolvoxHealth>();
            //Add some additional force towards volvox transform
            //then add force and disappearing movementspeed in the other direction, or just destroy?
            Debug.Log("I see food!");
        }
    }

    private void OnDestroy()     
    {
        //If out of sight of camera -> destroy
        //if(transform.)
    }

}
