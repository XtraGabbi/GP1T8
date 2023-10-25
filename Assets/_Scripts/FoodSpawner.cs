using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] Transform foodPrefab;
    private GameObject foodsParent;
    public Vector3 spawningRangeOffset;
    public float spawnRateSeconds;
    public int spawnLimit;

    private UnityEvent onTimerEnd;

    private IEnumerator FoodSpawning;

    // Start is called before the first frame update
    void Start()
    {
        FoodSpawning = StartFoodSpawning();

        CreateFoodParent();
        StartCoroutine(FoodSpawning);

        //Adds StopFoodSpawning as a listener to onTimerEnd
        onTimerEnd = GameObject.FindFirstObjectByType<GameTimer>().onTimerEnd;
        onTimerEnd.AddListener(StopFoodSpawning);
        onTimerEnd.AddListener(DestroyAllFood);
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
        StopCoroutine(FoodSpawning);        
    }



    // The spawning is reletive to the screen
    private void SpawnFood()
    {
        if(foodsParent.transform.childCount < spawnLimit)
        {
            Transform food = Instantiate(foodPrefab);
            food.transform.position = PlayerManager.Instance.RandomPointWithinProjectedRange() + spawningRangeOffset;//new Vector3(Random.Range(-spawningRange.x, spawningRange.x), 0, Random.Range(-spawningRange.y, spawningRange.y));
            food.transform.SetParent(foodsParent.transform, true);
        }
    }


    // Creates the object that will parent all the food prefab objects
    private void CreateFoodParent()
    {
        foodsParent = new GameObject();
        foodsParent.name = "Foods";
    }

    private void DestroyAllFood()
    {
        foreach(Transform food in foodsParent.transform)
        {  
            Destroy(food.gameObject); 
        }
    }
}
