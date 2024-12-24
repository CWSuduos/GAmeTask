using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    [Header("UI Components")]
    public Sprite[] sprites; // Список спрайтов для каждой сложности
    public Image imageComponent; // UI-элемент для отображения текущей сложности
    public Button nextButton; // Кнопка "Следующая сложность"
    public Button previousButton; // Кнопка "Предыдущая сложность"
    public Button confirmButton; // Кнопка "Подтвердить сложность"

    [Header("Settings")]
    public float delayBetweenClicks = 0.2f; // Задержка между переключениями сложностей
    public DifficultySettings difficultySettings; // Ссылка на настройки сложности

    private int currentSpriteIndex = 0; // Текущий индекс выбранной сложности
    private bool isChanging = false; // Флаг, предотвращающий спам кнопок

    public int CurrentSpriteIndex { get { return currentSpriteIndex; } }
    public DifficultySettings.DifficultyData CurrentDifficultyData { get; private set; } // Текущая сложность

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeDefaultDifficulty();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeDefaultDifficulty()
    {
        // Устанавливаем сохранённую сложность или лёгкую сложность по умолчанию
        currentSpriteIndex = PlayerPrefs.GetInt("DifficultyIndex", 0);
        if (difficultySettings != null)
        {
            CurrentDifficultyData = difficultySettings.GetDifficultyData(currentSpriteIndex);
            Debug.Log($"[DifficultyManager] Инициализирована сложность: индекс {currentSpriteIndex}, время {CurrentDifficultyData?.time}");
        }
        else
        {
            Debug.LogError("[DifficultyManager] DifficultySettings не назначен в инспекторе!");
        }
    }

    void Start()
    {
        if (!CheckComponents()) return; // Проверяем, все ли компоненты настроены

        SetupButtons(); // Настраиваем кнопки
        DisplaySprite(); // Отображаем текущий спрайт сложности
        UpdateButtonState(); // Обновляем состояние кнопок (включены/отключены)
        LogCurrentSprite(); // Логируем текущий спрайт
        UpdateCurrentDifficultyData(); // Обновляем данные текущей сложности
    }

    private bool CheckComponents()
    {
        bool isValid = true;

        // Проверяем наличие всех компонентов
        if (imageComponent == null)
        {
            Debug.LogError("[DifficultyManager] Image Component не назначен!");
            isValid = false;
        }

        if (sprites == null || sprites.Length == 0)
        {
            Debug.LogError("[DifficultyManager] Массив спрайтов пуст!");
            isValid = false;
        }

        if (nextButton == null)
        {
            Debug.LogError("[DifficultyManager] Кнопка 'Next' не назначена");
            isValid = false;
        }

        if (previousButton == null)
        {
            Debug.LogError("[DifficultyManager] Кнопка 'Previous' не назначена");
            isValid = false;
        }

        if (confirmButton == null)
        {
            Debug.LogError("[DifficultyManager] Кнопка 'Confirm' не назначена");
            isValid = false;
        }

        if (difficultySettings == null)
        {
            Debug.LogError("[DifficultyManager] Необходимо назначить DifficultySettings!");
            isValid = false;
        }

        return isValid;
    }

    private void SetupButtons()
    {
        // Привязываем методы к кнопкам
        nextButton.onClick.AddListener(NextSprite);
        previousButton.onClick.AddListener(PreviousSprite);
        confirmButton.onClick.AddListener(ConfirmDifficulty);
    }

    public void NextSprite()
    {
        if (isChanging) return; // Если уже переключаем, игнорируем
        StartCoroutine(ChangeSpriteWithDelay(1)); // Переключаем на следующий спрайт
    }

    public void PreviousSprite()
    {
        if (isChanging) return; // Если уже переключаем, игнорируем
        StartCoroutine(ChangeSpriteWithDelay(-1)); // Переключаем на предыдущий спрайт
    }

    private IEnumerator ChangeSpriteWithDelay(int direction)
    {
        isChanging = true; // Устанавливаем флаг
        int previousSpriteIndex = currentSpriteIndex;

        // Изменяем текущий индекс сложности
        currentSpriteIndex += direction;
        currentSpriteIndex = Mathf.Clamp(currentSpriteIndex, 0, sprites.Length - 1); // Ограничиваем индекс

        yield return new WaitForSeconds(delayBetweenClicks); // Задержка между переключениями

        DisplaySprite(); // Отображаем новый спрайт
        UpdateButtonState(); // Обновляем состояние кнопок
        LogSpriteChange(previousSpriteIndex); // Логируем изменение спрайта
        UpdateCurrentDifficultyData(); // Обновляем данные сложности
        isChanging = false; // Снимаем флаг
    }

    private void DisplaySprite()
    {
        // Устанавливаем текущий спрайт на Image Component
        if (imageComponent != null && currentSpriteIndex < sprites.Length)
        {
            imageComponent.sprite = sprites[currentSpriteIndex];
        }
    }

    private void UpdateButtonState()
    {
        // Включаем/выключаем кнопки Next и Previous в зависимости от текущего индекса
        previousButton.interactable = (currentSpriteIndex > 0);
        nextButton.interactable = (currentSpriteIndex < sprites.Length - 1);
    }

    private void LogCurrentSprite()
    {
        Debug.Log($"[DifficultyManager] Отображается спрайт: {sprites[currentSpriteIndex].name} (индекс: {currentSpriteIndex})");
    }

    private void LogSpriteChange(int previousSpriteIndex)
    {
        if (previousSpriteIndex != currentSpriteIndex)
        {
            Debug.Log($"[DifficultyManager] Спрайт изменен с: {sprites[previousSpriteIndex].name} (индекс: {previousSpriteIndex}) " +
                     $"на: {sprites[currentSpriteIndex].name} (индекс: {currentSpriteIndex})");
        }
        else
        {
            Debug.Log($"[DifficultyManager] Спрайт остался прежним: {sprites[currentSpriteIndex].name} (индекс: {currentSpriteIndex})");
        }
    }

    private void UpdateCurrentDifficultyData()
    {
        // Обновляем текущие данные сложности
        if (difficultySettings != null)
        {
            CurrentDifficultyData = difficultySettings.GetDifficultyData(currentSpriteIndex);
            if (CurrentDifficultyData != null)
            {
                Debug.Log($"[DifficultyManager] Обновлены данные сложности: Time = {CurrentDifficultyData.time}, Chance = {CurrentDifficultyData.chance}");
            }
            else
            {
                Debug.LogError("[DifficultyManager] Данные сложности не найдены для текущего индекса!");
            }
        }
    }

    private void ConfirmDifficulty()
    {
        // Сохраняем текущую сложность
        PlayerPrefs.SetInt("DifficultyIndex", currentSpriteIndex);
        PlayerPrefs.Save();

        Debug.Log($"[DifficultyManager] Подтверждена сложность: {sprites[currentSpriteIndex].name} " +
                 $"(индекс: {currentSpriteIndex}), Time = {CurrentDifficultyData.time}, " +
                 $"Chance = {CurrentDifficultyData.chance}");

        // Здесь можно добавить логику для обновления других систем (например, таймера)
    }

    public DifficultySettings.DifficultyData GetCurrentDifficultyData()
    {
        if (CurrentDifficultyData == null)
        {
            InitializeDefaultDifficulty();
        }
        return CurrentDifficultyData;
    }
}