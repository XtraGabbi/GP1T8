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
        StartCoroutine(StartFoodSpawning());
    }


    public IEnumerator StartFoodSpawning()
    {
        while (true)
        {
            SpawnFood();
            yield return new WaitForSeconds(spawnRateSeconds);
        }
    }
    public void StopFoodSpawning()
    {
        StopCoroutine(StartFoodSpawning());        
    }



    // The spawning is reletive to the center of the GameObject's position
    private void SpawnFood()
    {
        Transform food = Instantiate(foodPrefab);
        food.transform.position += new Vector3(Random.Range(-spawningRange.x, spawningRange.x), 0, Random.Range(-spawningRange.y, spawningRange.y));
    }

}
