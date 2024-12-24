using UnityEngine;
using UnityEngine.UI;
using System;

public class CountdownTimer : MonoBehaviour
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
            currentTime -= Time.deltaTime; // Уменьшаем время
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
        // Получаем данные текущей сложности из DifficultySettings
        if (DifficultySettings.Instance != null)
        {
            currentTime = DifficultySettings.Instance.GetCurrentDifficultyData().time;
            Debug.Log($"[CountdownTimer] Таймер установлен на: {currentTime} секунд");
            UpdateTimerText(); // Обновляем текстовое поле
            StartTimer(); // Запускаем таймер
        }
        else
        {
            Debug.LogError("[CountdownTimer] DifficultySettings не найден!");
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
            Debug.LogWarning("[CountdownTimer] Текстовое поле для таймера не привязано!");
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
        Debug.Log("[CountdownTimer] Таймер запущен");
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        Debug.Log("[CountdownTimer] Таймер остановлен");
    }

    private void OnTimerFinished()
    {
        Debug.Log("[CountdownTimer] Таймер завершён!");
        // Здесь можно добавить логику, которая выполняется после завершения таймера
    }
}