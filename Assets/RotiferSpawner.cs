using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotiferSpawner : MonoBehaviour
{
    public GameObject rotiferPrefab;
    public GameObject whirlpoolPrefab;
    public Whirlpool whirlpool;

    public float stayInterval = 3f;
    public float disappearInterval = 2f;

    // Start is called before the first frame update
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
        whirlpoolPrefab.transform.position = PlayerManager.Instance.RandomPointWithinProjectedRange();
        whirlpool.play = true;
        
        float timePassed = 0;
        while (timePassed < stayInterval)
        {
            timePassed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            // if (whirlpool.IsInDeadZone())
            // {
            //     Debug.Log("ready to eat");
            //     yield break;
            // }
        }

        whirlpool.play = false;
        Debug.Log("Stop Playing");
        yield return new WaitForSeconds(disappearInterval);
        StartCoroutine(Staying());
    }
    
    IEnumerator Eating()
    {
        float timePassed = 0;
        while (timePassed < stayInterval)
        {
            timePassed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            if (whirlpool.IsInDeadZone())
            {
                Debug.Log("ready to eat");
                yield break;
            }
        }

        whirlpool.play = false;
        Debug.Log("Stop Playing");
        
    }
    
    
}