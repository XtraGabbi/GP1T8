using System.Collections;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] Transform foodPrefab;
    private GameObject foodParent;
    [SerializeField] Vector2 spawningRange;
    public float spawnRateSeconds;
    public int spawnLimit;

    // Start is called before the first frame update
    void Start()
    {
        MakeFoodParent();
        StartCoroutine(StartFoodSpawning());
    }



    // Start food spawning
    public IEnumerator StartFoodSpawning()
    {
        while (true)
        {
            SpawnFood();
            yield return new WaitForSeconds(spawnRateSeconds);
        }
    }
    // Stop food spawning
    public void StopFoodSpawning()
    {
        StopCoroutine(StartFoodSpawning());        
    }



    // The spawning is reletive to the center of the GameObject's position
    private void SpawnFood()
    {
        if(foodParent.transform.childCount < spawnLimit)
        {
            Transform food = Instantiate(foodPrefab);
            food.transform.position += new Vector3(Random.Range(-spawningRange.x, spawningRange.x), 0, Random.Range(-spawningRange.y, spawningRange.y));
            food.transform.SetParent(foodParent.transform, true);
        }
    }


    // Makes the object that will parent all the food prefab objects
    private void MakeFoodParent()
    {
        foodParent = new GameObject();
        foodParent.name = "Foods";
    }
}