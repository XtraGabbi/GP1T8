using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotiferSpawner : MonoBehaviour
{
    [Header("Whirlpool")]
    public GameObject whirlpoolPrefab;
    public Whirlpool whirlpool;

    public float stayInterval = 3f;
    public float disappearInterval = 2f;

    [Header("Rotifer")]
    public GameObject rotiferPrefab;

    public float jumpHeightStart = -5;
    public float jumpHeightEnd = 3;
    public float attackInterval = 1;
    public float retreatInterval = 1;
    
    
    // Start is called before the first frame update

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + new Vector3(0, jumpHeightStart, 0), transform.position + new Vector3(0, jumpHeightEnd, 0));
    }

    void Start()
    {
    }

    private void OnEnable()
    {
        StartCoroutine(Staying());
    }


    // Update is called once per frame
    void Update()
    {
        // whirlpoolPrefab.transform.position = PlayerManager.Instance.RandomPointWithinProjectedRange();
    }

    IEnumerator Staying()
    {
        transform.position = PlayerManager.Instance.RandomPointWithinProjectedRange();
        whirlpool.play = true;
        whirlpool.isSucking = true;
        
        float timePassed = 0;
        while (timePassed < stayInterval)
        {
            timePassed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            if (whirlpool.IsInDeadZone())
            {
                Debug.Log("ready to eat");
                StartCoroutine(Eating());
                yield break;
            }
        }

        whirlpool.isSucking = false;
        whirlpool.play = false;
        Debug.Log("Stop Playing");
        yield return new WaitForSeconds(disappearInterval);
        StartCoroutine(Staying());
    }
    
    IEnumerator Eating()
    {
        float timePassed = 0;
        while (timePassed < attackInterval)
        {
            timePassed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            float currentHeight = Mathf.SmoothStep(jumpHeightStart, jumpHeightEnd, timePassed / attackInterval);
            rotiferPrefab.transform.localPosition = new Vector3(0, currentHeight, 0);
        }
        
         timePassed = 0;
        while (timePassed < retreatInterval)
        {
            timePassed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            float currentHeight = Mathf.SmoothStep(jumpHeightEnd, jumpHeightStart, timePassed / retreatInterval);
            rotiferPrefab.transform.localPosition = new Vector3(0, currentHeight, 0);
        }

        whirlpool.isSucking = false;
        whirlpool.play = false;
        yield return new WaitForSeconds(disappearInterval);
        StartCoroutine(Staying());
        
    }
    
    
}