using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolvoxSize : MonoBehaviour
{
    public static VolvoxSize instance;

    [SerializeField] float baseSize;
    [SerializeField] float sizeIncreesedBy;

    [SerializeField] int sizeUpRequirement;
    [SerializeField] int sizeLevel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        UpdateSize();
    }*/

    // Updates volvox size, sizeLevel & score
    public void UpdateSize()
    {
        UpdateSizeLevel();
        UpdateVolvoxSize();
    }

    private void UpdateVolvoxSize()
    {
        float setSize = (sizeLevel * sizeIncreesedBy) + baseSize;
        transform.localScale = new Vector3(setSize, setSize, setSize);
    }

    private void UpdateSizeLevel()
    {
        sizeLevel = Mathf.FloorToInt((float)Volvox.Instance.colonyCenter.childCount / sizeUpRequirement);
    }
}
