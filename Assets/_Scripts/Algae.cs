using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Algae : MonoBehaviour
{
    
    [Header("Lerp with self position")] 
    public GameObject lerpTarget;
    public float targetlerpSpeed = 0.1f;
    public float thislerpSpeed = 0.1f;
    
    [Header("For PID")]
    public Rigidbody rb;

    public float controlParam_Proportion;
    public float controlParam_Integration;
    public float controlParam_Differentiation;

    private Vector3 error = Vector3.zero;
    private Vector3 errorLastTime = Vector3.zero;
    private Vector3 integrationStored = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        movingTowardsLight();
    }


    
    void movingTowardsLight()
    {
        LerpPosition();
    }

    void PIDControl()
    {
        LightSource shiningLight = LightManager.instance.ShiningLight;
        
                error = shiningLight.transform.position - transform.position;
                integrationStored += error * Time.fixedDeltaTime;
                Vector3 force = controlParam_Proportion * error +
                                controlParam_Integration * integrationStored +
                                controlParam_Differentiation * (error - errorLastTime) / Time.fixedDeltaTime;
                rb.AddForce(force);
        
                errorLastTime = error;
    }

    void LerpPosition()
    {
        lerpTarget.transform.position = Vector3.Lerp(lerpTarget.transform.position,
            LightManager.instance.ShiningLight.transform.position, targetlerpSpeed * Time.deltaTime);
        
        transform.position = Vector3.Lerp(transform.position,
            lerpTarget.transform.position, thislerpSpeed * Time.deltaTime);
        
    }
}