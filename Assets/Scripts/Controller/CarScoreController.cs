using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance;

    private int score = 0;
    private int maxScore = 0; // ���� ��� ������������� �����
    public Text scoreText; // ���� ��� ��������� �����
    public Text maxScoreText; // ���� ��� ������������� �����
    public Text bonusScoreText; // ����� ���� ��� ��������� �����

    public List<GameObject> trackedObjects = new List<GameObject>();


    private void Start()
    {
        LoadMaxScore();
    }
    private void Update()
    {
        for (int i = trackedObjects.Count - 1; i >= 0; i--)
        {
            if (trackedObjects[i] == null)
            {
                AddScore(1);
                trackedObjects.RemoveAt(i);
            }
        }
    }

    public void AddScore(int points)
    {
        score += points;
        if (score > maxScore)
        {
            maxScore = score;
            SaveMaxScore(); // ���������� ������������� ����� � PlayerPrefs
        }
        UpdateScoreUI();
        Debug.Log($"{score}");
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{score}";
        }
        else
        {
            Debug.LogError("��������� ���� ��� ����� �� ���������!");
        }

        if (maxScoreText != null)
        {
            maxScoreText.text = $"Max: {maxScore}";
        }
        else
        {
            Debug.Log("��������� ���� ��� ������������� ����� �� ���������!");
        }

        // ��������� �������� ���� �� MaxScoreManager
        if (bonusScoreText != null)
        {
            bonusScoreText.text = $"{MaxScoreManager.Instance.GetMaxBonusScore()}";
        }
        else
        {
            
        }
    }

    private void LoadMaxScore()
    {
        maxScore = PlayerPrefs.GetInt("MaxScore", 0);
    }

    private void SaveMaxScore()
    {
        PlayerPrefs.SetInt("MaxScoreCar", maxScore);
        PlayerPrefs.Save();
    }

    public void TrackObject(GameObject obj)
    {
        if (!trackedObjects.Contains(obj))
        {
            trackedObjects.Add(obj);
        }
    }
}