using UnityEngine;
using UnityEngine.UI;
using System;

public class DifficultyBasedTimer : MonoBehaviour
{
    public Text timerText; // Ссылка на текстовое поле для отображения таймера
    private float currentTime; // Текущее время
    private bool isTimerRunning = false; // Флаг, указывает, работает ли таймер
    public static event Action OnTimerStop;

    private bool isInitialized = false; // Флаг, чтобы InitializeTimer вызывался только один раз с верными данными

    // Публичное свойство для чтения состояния таймера
    public bool IsTimerRunning => isTimerRunning;

    private void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();

            if (currentTime <= 0)
            {
                currentTime = 0;
                isTimerRunning = false;
                OnTimerFinished();
                GameOverUIManager gameOverManager = FindObjectOfType<GameOverUIManager>();
                if (gameOverManager != null)
                {
                    gameOverManager.ShowWinPanel();
                    gameOverManager.ShowResultPanel();
                }
                else
                {
                    Debug.LogError("GameOverUIManager не найден!");
                }
            }
        }
    }

    // Запускает таймер с текущей сложностью
    public void StartTimerWithDifficulty()
    {
        if (!isInitialized && DifficultySettings.Instance != null)
        {
            var difficultyData = DifficultySettings.Instance.GetCurrentDifficultyData();
            if (difficultyData != null)
            {
                currentTime = difficultyData.time;
                Debug.Log($"[DifficultyBasedTimer] Таймер установлен на: {currentTime} секунд");
                UpdateTimerText();
                isTimerRunning = true; // Запускаем таймер
                isInitialized = true;  // Помечаем, что инициализация произошла
                Debug.Log("[DifficultyBasedTimer] Таймер запущен");
            }
            else
            {
                Debug.LogError("[DifficultyBasedTimer] Данные сложности не найдены!");
            }
        }
        else if (isInitialized)
        {
            Debug.LogWarning("[DifficultyBasedTimer] Таймер уже был инициализирован.");
        }
        else
        {
            Debug.LogError("[DifficultyBasedTimer] DifficultySettings не найден!");
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            timerText.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
        }
        else
        {
            Debug.LogWarning("[DifficultyBasedTimer] Текстовое поле для таймера не привязано!");
        }
    }

    // Метод, который можно вызвать по нажатию кнопки
    public void OnStartButtonClicked()
    {
        StartTimerWithDifficulty();
    }

    // Публичный метод для остановки таймера
    public void StopTimer()
    {
        isTimerRunning = false;
        Debug.Log("[DifficultyBasedTimer] Таймер остановлен");
    }

    private void OnTimerFinished()
    {
        Debug.Log("[DifficultyBasedTimer] Таймер завершён!");
        OnTimerStop?.Invoke();
    }

    // Публичный метод для сброса таймера
    public void ResetTimer()
    {
        isTimerRunning = false;
        currentTime = 0f;
        UpdateTimerText();
        Debug.Log("[DifficultyBasedTimer] Таймер сброшен.");
    }
}