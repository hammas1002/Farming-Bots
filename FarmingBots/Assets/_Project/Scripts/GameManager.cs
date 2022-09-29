using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    int score;
    int topScore;

    public TMP_Text scoreUI;
    public TMP_Text topScoreUI;

    private void Awake()
    {
        if(instance==null) instance = this;
        topScore = PlayerPrefs.GetInt("topScore");
    }
    private void Start()
    {
        topScoreUI.text = topScore.ToString();
    }
    public static GameManager Instance
    {
        get
        {
            return instance;
        } 
    }

    public void UpdateScore(int _score)
    {
        score += _score;
        scoreUI.text = score.ToString();
        if (score> PlayerPrefs.GetInt("topScore"))
        {
            PlayerPrefs.SetInt("topScore", score);
        }    
    }

}
