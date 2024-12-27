using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Статическая переменная для хранения счёта
    public static int score = 0;

    // Ссылка на текстовое поле для отображения счёта
    public Text scoreText;

    // Метод для увеличения счёта
    public static void IncrementScore()
    {
        score++;
    }

    // Обновление текста в текстовом поле
    private void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = " " + score.ToString();
        }
    }
}
