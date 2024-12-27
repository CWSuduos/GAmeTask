using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // ����������� ���������� ��� �������� �����
    public static int score = 0;

    // ������ �� ��������� ���� ��� ����������� �����
    public Text scoreText;

    // ����� ��� ���������� �����
    public static void IncrementScore()
    {
        score++;
    }

    // ���������� ������ � ��������� ����
    private void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = " " + score.ToString();
        }
    }
}
