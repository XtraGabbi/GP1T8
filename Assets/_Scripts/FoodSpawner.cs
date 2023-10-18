using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] Transform foodPrefab;

    [SerializeField] Vector2 spawningRange;

    public float spawnRateSeconds;

    // Start is called before the first frame update
    void Start()
    {
        StartFoodSpawning();
    }

    // Update is called once per frame
    void Update()
    {       
        

    }

    public void StartFoodSpawning()
    {
        InvokeRepeating("SpawnFood", 0, spawnRateSeconds);
    }
    public void StopFoodSpawning()
    {
        CancelInvoke("SpawnFood");
    }

    // The spawning is reletive to the center of the GameObject's position
    private void SpawnFood()
    {
        Transform food = Instantiate(foodPrefab);
        food.transform.position += new Vector3(Random.Range(-spawningRange.x, spawningRange.x), 0, Random.Range(-spawningRange.y, spawningRange.y));
    }

}
