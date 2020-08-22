using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text scoreUI;
    public TMP_Text highScoreUI;
    int money;
    private void Start() 
    {
        money = PlayerPrefs.GetInt("money");
        scoreUI.text = "Score : " + money.ToString();

        if (PlayerPrefs.HasKey("highScore"))
        {
            if (money > PlayerPrefs.GetInt("highScore"))
            {
                PlayerPrefs.SetInt("highScore", money);
            }
        }
        else
            PlayerPrefs.SetInt("highScore", money);

        highScoreUI.text = "High Score : " + PlayerPrefs.GetInt("highScore").ToString();
    }
}
