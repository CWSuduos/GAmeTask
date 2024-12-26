using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShaperTimer : MonoBehaviour
{
    private float currentTime; // Текущее время таймера
    public Text timerText;     // Ссылка на текстовый объект UI для отображения таймера

   public void Start()
   {
       // Получаем время из текущей настройки сложности
       if (DifficultySettings.Instance != null && DifficultySettings.Instance.StartTime)
       {
           currentTime = DifficultySettings.Instance.GetCurrentDifficultyData().time;
       }
       else
       {
           Debug.Log("DifficultySettings не настроен или StartTime не установлен!");
           currentTime = 120f; // Значение по умолчанию
       }
  
       // Запускаем корутину для обновления таймера каждую секунду
       StartCoroutine(UpdateTimer());
   }

    public IEnumerator UpdateTimer()
    {
        // Пока таймер больше 0
       
        while (currentTime > 0)
        {
            // Уменьшаем текущее время на 1 секунду
            currentTime--;

            // Обновляем отображение таймера
            UpdateTimerText();

            // Ждём 1 секунду
            yield return new WaitForSeconds(1f);
        }

        // Когда таймер достигнет 0
        TimerFinished();
    }

    private void UpdateTimerText()
    {
        // Преобразуем оставшееся время в формат MM:SS
        int minutes = Mathf.FloorToInt(currentTime / 60); // Минуты
        int seconds = Mathf.FloorToInt(currentTime % 60); // Секунды

        // Обновляем текстовый объект UI
        if (timerText != null)
        {
            timerText.text = $"{minutes:00}:{seconds:00}"; // Формат MM:SS
        }
    }
    public GameOverUIManager gameOverUIManager;
    private void TimerFinished()
    {
        // Действия, которые нужно выполнить, когда таймер дошёл до 0
        Debug.Log("Таймер завершён!");
        if (timerText != null)
        {
            gameOverUIManager.ShowLosePanel();
            gameOverUIManager.ShowResultPanel();
            timerText.text = "00:00"; // Устанавливаем текст на 00:00
        }
    }
}