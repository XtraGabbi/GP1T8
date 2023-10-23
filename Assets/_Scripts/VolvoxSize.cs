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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateSize();
    }

    private void UpdateSize()
    {
        sizeLevel = Mathf.CeilToInt(addedFoodTEMP / sizeUpAmount);

        float setSize = (sizeLevel * sizeIncreesedBy) + baseSize;
        transform.localScale = new Vector3(setSize, setSize, setSize);
    }
}
