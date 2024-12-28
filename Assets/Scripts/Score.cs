using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // ����������� ���������� ��� �������� ������������� �����
    public static int maxScore = 0;
    public static int score = 0;
    // ������ �� ��������� ���� ��� ����������� �����
    public Text scoreText;
    public Text scoreText1;
    // ������ �� ��������� ���� ��� ����������� ������������� �����
    public Text maxScoreText;

    // ����� ��� ���������� �����
    public static void IncrementScore()
    {
        score++;
        if (score > maxScore)
        {
            maxScore = score;
        }
    }

    // ���������� ������ � ��������� �����
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
