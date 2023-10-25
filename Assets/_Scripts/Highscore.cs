using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Highscore : MonoBehaviour
{
    public static Highscore Instance;

    [SerializeField] HighscoreData HighscoreData;

    private UnityEvent onTimerEnd;

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

        //Adds UpdateHighScore as a listener to onTimerEnd
        onTimerEnd = GameObject.FindFirstObjectByType<GameTimer>().onTimerEnd;
        onTimerEnd.AddListener(UpdateHighscore);
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
