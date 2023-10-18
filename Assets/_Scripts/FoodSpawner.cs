using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public Transform foodPrefab;

    public Vector2 spawningRange;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnFood();
        }
    }

    // Update is called once per frame
    void Update()
    {       


    }

    private void SpawnFood()
    {
        Transform food = Instantiate(foodPrefab);
        food.transform.position += new Vector3(Random.Range(-spawningRange.x, spawningRange.x), 0, Random.Range(-spawningRange.y, spawningRange.y));
    }

}
