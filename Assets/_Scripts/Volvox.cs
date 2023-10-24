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
    public float maxSpeed = 25f;
    public bool isFollowing = true;

    // [Header("Lerp with self position")] public float lerpSpeed = 0.1f;

    [Header("For Movement Control")] public Rigidbody rb;

    [FormerlySerializedAs("controlParam_Proportion")]
    public float speedFactor = 0.01f;

    [FormerlySerializedAs("controlParam_Differentiation")]
    public float preventOvershootingFactor = 0.42f;

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
        rb.maxLinearVelocity = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = new Vector3(10, 10, 10) * Time.deltaTime;
        colonyCenter.transform.rotation *= Quaternion.Euler(rotation);
    }

    private void FixedUpdate()
    {
        if (isFollowing)
        {
            PIDControl(null, null);
        }
    }


    // void movingTowardsLight()
    // {
    //     LerpPosition();
    // }

    void PIDControl(Transform current, Transform target)
    {
        error = _followTarget.transform.position - transform.position;
        Vector3 force = speedFactor * error +
                        preventOvershootingFactor * (error - errorLastTime) / Time.fixedDeltaTime;
        rb.AddForce(force);

        errorLastTime = error;
    }

    // void LerpPosition()
    // {
    //
    //     transform.position = Vector3.Lerp(transform.position,
    //         _followTarget.transform.position, lerpSpeed * Time.deltaTime);
    // }

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

        VolvoxSize.instance.UpdateSize();
    }

    public void RemoveColony()
    {
        if (colonyCenter.childCount > 0)
        {
            GameObject colony = colonyCenter.GetChild(colonyCenter.childCount - 1).gameObject;
            Destroy(colony);
            print("colony sucked!");
        }

        VolvoxSize.instance.UpdateSize();
    }
}