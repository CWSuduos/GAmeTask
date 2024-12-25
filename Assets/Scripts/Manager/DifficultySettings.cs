using UnityEngine;
using System.Collections.Generic;

public class DifficultySettings : MonoBehaviour
{
    public static DifficultySettings Instance { get; private set; }

    [System.Serializable]
    public class DifficultyData
    {
        public float time;   // Время таймера
        public float chance; // Дополнительный параметр (например, вероятность события)
    }

    [Header("Настройки сложности")]
    public List<DifficultyData> difficultySettings = new List<DifficultyData>();
    public bool StartTime {  get; private set; }

    private int currentDifficultyIndex = 0;

    private void Awake()
    {
        // Настройка Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Если список сложностей пуст, добавляем значения по умолчанию
        if (difficultySettings == null || difficultySettings.Count == 0)
        {
            difficultySettings = new List<DifficultyData>
            {
                new DifficultyData { time = 180f, chance = 0.2f },
                new DifficultyData { time = 240f, chance = 0.15f },
                new DifficultyData { time = 300f, chance = 0.1f }
            };
        }
        StartTime = false;
    }

    // Возвращает данные сложности по индексу
    public DifficultyData GetDifficultyData(int difficultyIndex)
    {
        // Проверяем, что индекс корректный
        if (difficultyIndex >= 0 && difficultyIndex < difficultySettings.Count)
        {
            return difficultySettings[difficultyIndex];
        }
        else
        {
            Debug.LogWarning($"[DifficultySettings] Некорректный индекс сложности: {difficultyIndex}");
            return null; // Возвращаем null, если индекс некорректный
        }
    }

    // Возвращает данные текущей сложности
    public DifficultyData GetCurrentDifficultyData()
    {
        return GetDifficultyData(currentDifficultyIndex);
    }

    // Устанавливает текущую сложность
    public void SetDifficulty(int difficultyIndex)
    {
        if (difficultyIndex >= 0 && difficultyIndex < difficultySettings.Count)
        {
            currentDifficultyIndex = difficultyIndex;
            StartTime = true;
            Debug.Log($"[DifficultySettings] Установлена сложность: {difficultyIndex}, Время: {difficultySettings[difficultyIndex].time}");
        }
        else
        {
            Debug.LogWarning($"[DifficultySettings] Некорректный индекс сложности: {difficultyIndex}");
        }
    }

}