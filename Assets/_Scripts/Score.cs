using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance;

    public int score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void UpdateScore()
    {
        score = Volvox.Instance.colonyCenter.childCount;
    }
}
