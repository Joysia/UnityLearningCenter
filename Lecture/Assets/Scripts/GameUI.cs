using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Text txtScore;
    private int totScore = 0;
    public Text gagueText;

    // Use this for initialization
    private void Start()
    {
        totScore = PlayerPrefs.GetInt("TOT_SCORE", 0);
        DispScore(0);
    }

    public void DispScore(int score)
    {
        totScore += score;
        txtScore.text = "score <color=#ff0000>" + totScore.ToString() + "</color>";
        PlayerPrefs.SetInt("TOT_SCORE", totScore);
    }

    public void DataReset()
    {
        totScore = 0;
        DispScore(0);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}