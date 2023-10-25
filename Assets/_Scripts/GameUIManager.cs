using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    [SerializeField] TMP_Text UIScoreText;

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
    }

    public void UpdateUIScore()
    {
        UIScoreText.text = Volvox.Instance.colonyCenter.childCount.ToString();
    }


}
