using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FoodSpawnSystem : MonoBehaviour
{
    public GameObject foodPrefab;
    public List<Transform> spawnPoints = new List<Transform>();
    public int maxFoodInstances = 2;
    public int minFoodInstances = 1; // Minimum number of food instances
    public string foodTag = "Food"; // Tag assigned to food items
    public float spawnRadius = 1.0f; // Adjust this value as needed

    private List<GameObject> activeFoodInstances = new List<GameObject>();
    private List<Transform> shuffledSpawnPoints = new List<Transform>();
    private int currentSpawnPointIndex = 0;

    private void Awake()
    {
        // Find all predefined spawn points in the scene
        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach (GameObject spawnPoint in spawnPointObjects)
        {
            spawnPoints.Add(spawnPoint.transform);
        }

        // Shuffle the spawn points to randomize their order
        shuffledSpawnPoints = spawnPoints.OrderBy(x => Random.value).ToList();

        // Spawn initial food instances up to the minimum
        for (int i = 0; i < minFoodInstances; i++)
        {
            TrySpawnFood();
        }
    }

    private void Update()
    {
        // Spawn food to reach the minimum if needed
        while (activeFoodInstances.Count < minFoodInstances)
        {
            TrySpawnFood();
        }
    }

    private void TrySpawnFood()
    {
        if (activeFoodInstances.Count < maxFoodInstances)
        {
            if (currentSpawnPointIndex >= shuffledSpawnPoints.Count)
            {
                // If we've used all spawn points, shuffle again
                shuffledSpawnPoints = spawnPoints.OrderBy(x => Random.value).ToList();
                currentSpawnPointIndex = 0;
            }

            Transform spawnPoint = shuffledSpawnPoints[currentSpawnPointIndex];

            // Check for overlapping food items in the vicinity
            Collider[] colliders = Physics.OverlapSphere(spawnPoint.position, spawnRadius);
            bool canSpawn = true;

            foreach (Collider col in colliders)
            {
                if (col.CompareTag(foodTag))
                {
                    canSpawn = false;
                    break;
                }
            }

            if (canSpawn)
            {
                GameObject foodInstance = Instantiate(foodPrefab, spawnPoint.position, Quaternion.identity);
                foodInstance.tag = foodTag; // Assign the foodTag to the new instance
                activeFoodInstances.Add(foodInstance);
            }

            currentSpawnPointIndex++;
        }
    }




    public void FoodDestroyed(GameObject foodInstance)
    {
        activeFoodInstances.Remove(foodInstance);
        TrySpawnFood(); // Spawn a new food item when one is destroyed
        Debug.Log("fooddestroy triggered");
    }

}