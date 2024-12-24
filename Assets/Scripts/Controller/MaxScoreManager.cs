using UnityEngine;
using UnityEngine.UI;

public class MaxScoreManager : MonoBehaviour
{
    public static MaxScoreManager Instance { get; private set; }

    [Header("UI")]
    public Text maxScoreText; // Текстовое поле для отображения максимального счёта
    public Text maxBonusScoreText; // Текстовое поле для отображения максимального бонусного счёта

    private int maxScore = 0; // Максимальный счёт
    private int maxBonusScore = 0; // Максимальный бонусный счёт

   
    private void Start()
    {
        LoadMaxScore();
        LoadMaxBonusScore();
        UpdateMaxScoreDisplay();
        UpdateMaxBonusScoreDisplay();
    }

    /// <summary>
    /// Возвращает текущий максимальный счёт.
    /// </summary>
    public int GetMaxScore()
    {
        return maxScore;
    }

    /// <summary>
    /// Обновляет максимальный счёт и UI.
    /// </summary>
    public void UpdateMaxScore(int newScore)
    {
        if (newScore > maxScore)
        {
            maxScore = newScore;
            SaveMaxScore();
            UpdateMaxScoreDisplay();
            Debug.Log($"[MaxScoreManager] Новый максимальный счёт: {maxScore}");
        }
    }

    /// <summary>
    /// Загружает максимальный счёт из PlayerPrefs.
    /// </summary>
    private void LoadMaxScore()
    {
        maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        Debug.Log($"[MaxScoreManager] Загружен максимальный счёт: {maxScore}");
    }

    /// <summary>
    /// Сохраняет максимальный счёт в PlayerPrefs.
    /// </summary>
    private void SaveMaxScore()
    {
        PlayerPrefs.SetInt("MaxScore", maxScore);
        PlayerPrefs.Save();
        Debug.Log("[MaxScoreManager] Максимальный счёт сохранён.");
    }

    /// <summary>
    /// Обновляет текстовое поле для максимального счёта.
    /// </summary>
    private void UpdateMaxScoreDisplay()
    {
        if (maxScoreText != null)
        {
            maxScoreText.text = $"{maxScore}";
        }
        else
        {
            Debug.LogWarning("[MaxScoreManager] Текстовое поле для максимального счёта не назначено!");
        }
    }

    /// <summary>
    /// Обновляет максимальный бонусный счёт и UI.
    /// </summary>
    public void UpdateMaxBonusScore(int newBonusScore)
    {
        if (newBonusScore > maxBonusScore)
        {
            maxBonusScore = newBonusScore;
            SaveMaxBonusScore();
            UpdateMaxBonusScoreDisplay();
            Debug.Log($"[MaxScoreManager] Новый максимальный бонусный счёт: {maxBonusScore}");
        }
    }

    /// <summary>
    /// Возвращает текущий максимальный бонусный счёт.
    /// </summary>
    public int GetMaxBonusScore()
    {
        return maxBonusScore;
    }

    /// <summary>
    /// Загружает максимальный бонусный счёт из PlayerPrefs.
    /// </summary>
    private void LoadMaxBonusScore()
    {
        maxBonusScore = PlayerPrefs.GetInt("MaxBonusScore", 0);
        Debug.Log($"[MaxScoreManager] Загружен максимальный бонусный счёт: {maxBonusScore}");
    }

    /// <summary>
    /// Сохраняет максимальный бонусный счёт в PlayerPrefs.
    /// </summary>
    private void SaveMaxBonusScore()
    {
        PlayerPrefs.SetInt("MaxBonusScore", maxBonusScore);
        PlayerPrefs.Save();
        Debug.Log("[MaxScoreManager] Максимальный бонусный счёт сохранён.");
    }

    /// <summary>
    /// Обновляет текстовое поле для максимального бонусного счёта.
    /// </summary>
    private void UpdateMaxBonusScoreDisplay()
    {
        if (maxBonusScoreText != null)
        {
            maxBonusScoreText.text = $"{maxBonusScore}";
        }
        else
        {
            Debug.LogWarning("[MaxScoreManager] Текстовое поле для максимального бонусного счёта не назначено!");
        }
    }
}