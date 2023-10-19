using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampyrellaSpawner : MonoBehaviour
{
    [SerializeField] private GameObject vampyrellaPrefab;
    [SerializeField] private float vampyrellaSpawnInterval = 3f;
    
    void Start()
    {
        StartCoroutine(spawnVampyrella(vampyrellaSpawnInterval, vampyrellaPrefab));
    }

    private IEnumerator spawnVampyrella(float interval, GameObject vampyrella)
    {
        yield return new WaitForSeconds(interval);
        GameObject newVampyrella = Instantiate(vampyrella, transform.position, Quaternion.identity);
        newVampyrella.transform.SetParent(transform);
        newVampyrella.name = "Vampyrella";
        newVampyrella.transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(1, 179), 0);        //This is where to change or duplicate for enemies from other side.
        StartCoroutine(spawnVampyrella(interval, newVampyrella));

        // Debug.Log(newVampyrella.transform.rotation);
        // Debug.Log("Vampyrella Spawned!");
    }
}
