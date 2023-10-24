using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscore : MonoBehaviour
{
    public static Highscore Instance;

    [SerializeField] HighscoreData HighscoreData;

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

    public void UpdateHighscore()
    {
        if (Volvox.Instance.colonyCenter.childCount > HighscoreData.highscore)
        {
            HighscoreData.highscore = Volvox.Instance.colonyCenter.childCount;
        }
    }
    public void ResetHighscore() 
    {
        HighscoreData.highscore = 0;
    }
}
