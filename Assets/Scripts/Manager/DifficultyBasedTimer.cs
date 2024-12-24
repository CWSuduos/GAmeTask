using UnityEngine;
using UnityEngine.UI;
using System;

public class DifficultyBasedTimer : MonoBehaviour
{
    public Text timerText; // Ссылка на текстовое поле для отображения таймера
    private float currentTime; // Текущее время таймера
    private bool isTimerRunning = false; // Флаг, указывает, работает ли таймер

    private void Start()
    {
        InitializeTimer(); // Инициализируем таймер при запуске
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime; // Уменьшаем текущее время
            UpdateTimerText(); // Обновляем текстовое поле

            if (currentTime <= 0) // Если время закончилось
            {
                currentTime = 0; // Устанавливаем время на 0
                isTimerRunning = false; // Останавливаем таймер
                OnTimerFinished(); // Вызываем обработчик завершения таймера
            }
        }
    }

    public void InitializeTimer()
    {
        // Получаем данные сложности из DifficultyManager
        if (DifficultyManager.Instance != null)
        {
            var difficultyData = DifficultyManager.Instance.GetCurrentDifficultyData();
            if (difficultyData != null)
            {
                currentTime = difficultyData.time; // Устанавливаем время из данных сложности
                Debug.Log($"[DifficultyBasedTimer] Таймер установлен на: {currentTime} секунд");
                UpdateTimerText(); // Обновляем текстовое поле
                StartTimer(); // Запускаем таймер
            }
            else
            {
                Debug.LogError("[DifficultyBasedTimer] Данные сложности не найдены!");
            }
        }
        else
        {
            Debug.LogError("[DifficultyBasedTimer] DifficultyManager не найден!");
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            // Форматируем время в формате MM:SS
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            timerText.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
        }
        else
        {
            Debug.LogWarning("[DifficultyBasedTimer] Текстовое поле для таймера не привязано!");
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
        Debug.Log("[DifficultyBasedTimer] Таймер запущен");
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        Debug.Log("[DifficultyBasedTimer] Таймер остановлен");
    }

    private void OnTimerFinished()
    {
        Debug.Log("[DifficultyBasedTimer] Таймер завершён!");
        // Здесь можно добавить логику, которая выполняется после завершения таймера
    }
}