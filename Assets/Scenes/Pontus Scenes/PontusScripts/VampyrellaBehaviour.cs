using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampyrellaBehaviour : MonoBehaviour
{
    [SerializeField] GameObject vampyrella;
    
    public Rigidbody rb;
    private float movementSpeed = 2f;

    public GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    void FixedUpdate()
    {
        VampyrellaMovement();
    }

    void VampyrellaMovement()
    {
        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);     //Moves Vamp in straight line
        
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
            //Add some additional force towards volvox transform
            //then add force and disappearing movementspeed in the other direction, or just destroy?
            Debug.Log("meet enemy!");
        }
    }

    private void OnDestroy()     
    {
        //If out of sight of camera -> destroy
        //if(transform.)
    }

}
