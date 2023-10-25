using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    [SerializeField] TMP_Text UIScoreText;
    [SerializeField] TMP_Text UIHighscoreText;
    [SerializeField] HighscoreData highscoreData;
    private UnityEvent onTimerEnd;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        UpdateUIScore();

        //Adds RevealUIHighscore as a listener to onTimerEnd
        onTimerEnd = GameObject.FindFirstObjectByType<GameTimer>().onTimerEnd;
        onTimerEnd.AddListener(RevealUIHighscore);
    }

    public void UpdateUIScore()
    {
        UIScoreText.text = Volvox.Instance.colonyCenter.childCount.ToString();
    }

    private void RevealUIHighscore()
    {
        UIHighscoreText.text = "BEST: " + highscoreData.highscore.ToString();
    }


}
