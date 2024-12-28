using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UI")]
    public Text scoreText; // Ссылка на текстовый элемент UI для отображения очков
    public Text maxScoreText; // Ссылка на текст для отображения максимального счёта
    public Text scoreText1;
    private int currentScore = 0; // Текущий счёт игрока
    private int maxScore = 0;     // Максимальный счёт

    public event Action OnInitialized; // Событие при инициализации ScoreManager

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadMaxScore(); // Загружаем максимальный счёт
            InitializeScore();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Инициализация счёта при старте.
    /// </summary>
    private void InitializeScore()
    {
        currentScore = 0;
        UpdateScoreDisplay();
        UpdateMaxScoreDisplay();
        Debug.Log("[ScoreManager] Счёт инициализирован: 0");
    }

    /// <summary>
    /// Добавляет очки при сборе особого объекта.
    /// </summary>
    /// <param name="points">Количество очков для добавления.</param>
    /// <param name="objectName">Название объекта (для отладки).</param>
    public void AddPoints(int points, string objectName = "")
    {
        currentScore += points;
        UpdateScoreDisplay();

        // Проверяем и обновляем максимальный счёт
        if (currentScore > maxScore)
        {
            maxScore = currentScore;
            SaveMaxScore();
            UpdateMaxScoreDisplay();
        }

        Debug.Log($"[ScoreManager] Новый счёт: {currentScore}"); // Лог для проверки
    }

    /// <summary>
    /// Обновляет отображение текущего счёта на UI.
    /// </summary>
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}";
            scoreText1.text = $"{currentScore}";
        }
        else
        {
            Debug.LogWarning("[ScoreManager] Текстовое поле для счёта не назначено!");
        }
    }

    /// <summary>
    /// Обновляет отображение максимального счёта на UI.
    /// </summary>
    private void UpdateMaxScoreDisplay()
    {
        if (maxScoreText != null)
        {
            maxScoreText.text = $"{maxScore}";
        }
        else
        {
            Debug.LogWarning("[ScoreManager] Текстовое поле для максимального счёта не назначено!");
        }
    }

    /// <summary>
    /// Возвращает текущий счёт игрока.
    /// </summary>
    public int GetCurrentScore()
    {
        return currentScore;
    }

    /// <summary>
    /// Возвращает максимальный счёт игрока.
    /// </summary>
    public int GetMaxScore()
    {
        return maxScore;
    }

    /// <summary>
    /// Сбрасывает текущий счёт на ноль.
    /// </summary>
    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreDisplay();
        Debug.Log("[ScoreManager] Счёт был сброшен.");
    }

    /// <summary>
    /// Загружает максимальный счёт из PlayerPrefs.
    /// </summary>
    private void LoadMaxScore()
    {
        maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        Debug.Log($"[ScoreManager] Загружен максимальный счёт: {maxScore}");
    }

    /// <summary>
    /// Сохраняет максимальный счёт в PlayerPrefs.
    /// </summary>
    private void SaveMaxScore()
    {
        PlayerPrefs.SetInt("MaxScore", maxScore);
        PlayerPrefs.Save();
        Debug.Log($"[ScoreManager] Новый максимальный счёт сохранён: {maxScore}");
    }
}