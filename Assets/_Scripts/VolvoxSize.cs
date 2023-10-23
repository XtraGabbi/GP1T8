using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolvoxSize : MonoBehaviour
{
    [SerializeField] float baseSize;
    [SerializeField] float sizeIncreesedBy;

    [SerializeField] int sizeUpAmount;
    [SerializeField] int sizeLevel;

    [SerializeField] int addedFoodTEMP; // to be removed later

    private VolvoxColonyCount colonyCount;

    // Start is called before the first frame update
    void Start()
    {
        colonyCount = GetComponent<VolvoxColonyCount>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSize();
    }

    private void UpdateSize()
    {
        sizeLevel = Mathf.FloorToInt((float)colonyCount.colonyCount / sizeUpAmount);

        float setSize = (sizeLevel * sizeIncreesedBy) + baseSize;
        transform.localScale = new Vector3(setSize, setSize, setSize);
    }
}
