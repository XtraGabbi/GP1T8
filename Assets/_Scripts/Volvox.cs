using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Volvox : MonoBehaviour
{
    public static Volvox Instance;

    public GameObject colonyPrefab;
    public Transform colonyCenter;

    [Header("Lerp with self position")] public float lerpSpeed = 0.1f;

    [Header("For PID")] public Rigidbody rb;

    public float controlParam_Proportion = 0.01f;
    public float controlParam_Differentiation = 0.42f;

    private Vector3 error = Vector3.zero;
    private Vector3 errorLastTime = Vector3.zero;

    private LightManager _lightManager;
    private FollowTarget _followTarget;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _lightManager = LightManager.Instance;
        _followTarget = FollowTarget.Instance;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = new Vector3(10, 10, 10) * Time.deltaTime;
        colonyCenter.transform.rotation *= Quaternion.Euler(rotation);
    }

    private void FixedUpdate()
    {
        PIDControl(null,null);
    }


    void movingTowardsLight()
    {
        LerpPosition();
    }

    void PIDControl(Transform current, Transform target)
    {
        error = _followTarget.transform.position - transform.position;
        Vector3 force = controlParam_Proportion * error +
                        controlParam_Differentiation * (error - errorLastTime) / Time.fixedDeltaTime;
        rb.AddForce(force);
    
        errorLastTime = error;
    }
    
    void LerpPosition()
    {
    
        transform.position = Vector3.Lerp(transform.position,
            _followTarget.transform.position, lerpSpeed * Time.deltaTime);
    }

    public void AddColony()
    {
        float randomDist = Random.Range(0f, 0.9f);
        float randomX = Random.Range(0f, 360f);
        float randomY = Random.Range(0f, 360f);
        float randomZ = Random.Range(0f, 360f);

        Vector3 randomPos = Quaternion.Euler(randomX, randomY, randomZ) * Vector3.forward * randomDist;
        
        GameObject newColony = Instantiate(colonyPrefab);
        newColony.transform.SetParent(colonyCenter);
        newColony.transform.position += colonyCenter.position + randomPos;

        colonyCount++; //added this to keep track of the count of the colony - Elliott
    }
}