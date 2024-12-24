using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance;

    private int score = 0;
    public Text scoreText; // Поле для основного счета
    public Text bonusScoreText; // Новое поле для бонусного счета

    public List<GameObject> trackedObjects = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
            Debug.LogError("Текстовое поле для счёта не назначено!");
        }

        // Обновляем бонусный счет из MaxScoreManager
        if (bonusScoreText != null)
        {
            bonusScoreText.text = $"{MaxScoreManager.Instance.GetMaxBonusScore()}";
        }
        else
        {
            Debug.LogWarning("Текстовое поле для бонусного счёта не назначено!");
        }
    }

    public void TrackObject(GameObject obj)
    {
        if (!trackedObjects.Contains(obj))
        {
            trackedObjects.Add(obj);
        }
    }
}