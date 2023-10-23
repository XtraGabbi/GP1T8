using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolvoxSize : MonoBehaviour
{
    [SerializeField] float baseSize;
    [SerializeField] float sizeIncreesedBy;

    [SerializeField] int sizeUpRequirement;
    [SerializeField] int sizeLevel;

    // Update is called once per frame
    void Update()
    {
        UpdateSize();
    }

    // Updates volvox size & sizeLevel
    private void UpdateSize()
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
