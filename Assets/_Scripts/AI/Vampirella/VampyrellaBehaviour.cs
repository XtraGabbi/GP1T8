using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampyrellaBehaviour : MonoBehaviour
{
    [Header("Vampyrella Settings")]
    [SerializeField] float movementSpeed;
    [SerializeField] float giveUpTimer;
    [SerializeField] int damage;
    [Space(15)]
    
    [SerializeField] GameObject player;
    bool _seePlayer;
    float _timer;

    private void Awake()
    {
        
    }

    void FixedUpdate()
    {
        VampyrellaMovement();
    }

    void VampyrellaMovement()
    {
        if (!_seePlayer)
        {
            gameObject.transform.Translate(Vector3.forward * (Time.fixedDeltaTime * movementSpeed));
        }
        else
        {
            gameObject.transform.LookAt(player.transform);
            gameObject.transform.Translate(Vector3.forward * (Time.fixedDeltaTime * movementSpeed));
            _timer += Time.fixedDeltaTime;
        }

        if (giveUpTimer < _timer)
        {
            _seePlayer = false;
            Debug.Log("Give up");
            _timer = 0f;
        }
        //Want it to move towards player
        //rb.MovePosition(player.transform.position*movementSpeed);       //This moves it directly to the player
    }

    private void DetectVolvox()
    {
        //Attatch to volvox if close enough. --> detection, raycast?
    }
    
    

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<VolvoxHealth>()) 
        {
            _seePlayer = true;
            player = collision.GetComponent<VolvoxHealth>().gameObject;
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
