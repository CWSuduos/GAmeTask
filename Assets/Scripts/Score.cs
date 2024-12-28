using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Статическая переменная для хранения максимального счёта
    public static int maxScore = 0;
    public static int score = 0;
    // Ссылка на текстовое поле для отображения счёта
    public Text scoreText;
    public Text scoreText1;
    // Ссылка на текстовое поле для отображения максимального счёта
    public Text maxScoreText;

    // Метод для увеличения счёта
    public static void IncrementScore()
    {
        score++;
        if (score > maxScore)
        {
            maxScore = score;
        }
    }

    // Обновление текста в текстовых полях
    private void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
            scoreText1.text = score.ToString();
        }

        if (maxScoreText != null)
        {
            maxScoreText.text = "Max: " + maxScore.ToString();
        }
    }
}
