using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Highscore : MonoBehaviour
{
    public static Highscore Instance;

    [SerializeField] HighscoreData highscoreData;

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
        if (Volvox.Instance.colonyCenter.childCount > highscoreData.highscore)
        {
            highscoreData.highscore = Volvox.Instance.colonyCenter.childCount;
        }
    }
    public void ResetHighscore() 
    {
        highscoreData.highscore = 0;
    }
}
