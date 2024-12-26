using UnityEngine;
using UnityEngine.UI;

public class ScoreCsomosTimer : MonoBehaviour
{
    public Text scoreText;      // Текстовое поле для текущего счёта
    public Text maxScoreText;   // Текстовое поле для максимального счёта
    public Button startButton;  // Кнопка для запуска счётчика

    private int maxScore = 0;   // Максимальный счёт
    private int score = 0;      // Текущий счёт
    private bool isCounting = false;    // Флаг, указывает, запущен ли счётчик
    private bool isPlayerAlive = true;  // Флаг состояния игрока

    private float timer = 0f;   // Таймер для отсчёта секунд

    private DifficultyBasedTimer difficultyTimer; // Ссылка на DifficultyBasedTimer

    void Start()
    {
        // Загружаем максимальный счёт из PlayerPrefs
        maxScore = PlayerPrefs.GetInt("MaxScoreCosmos", 0);

        // Находим DifficultyBasedTimer при старте
        difficultyTimer = FindObjectOfType<DifficultyBasedTimer>();

        // Назначаем действие для кнопки
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartCounting);
        }

        // Подписываемся на событие смерти игрока
        PlayerHealthCosmos.OnPlayerDeath.AddListener(OnPlayerDeath);

        UpdateScoreText();
        UpdateMaxScoreText();
    }

    void Update()
    {
        if (isCounting && isPlayerAlive)
        {
            // Проверяем, не остановлен ли таймер
            if (difficultyTimer != null && !difficultyTimer.IsTimerRunning)
            {
                StopCounting();
                return; // Выходим из Update, чтобы избежать дальнейшего увеличения счета
            }

            timer += Time.deltaTime;

            // Каждая прошедшая секунда
            if (timer >= 1f)
            {
                score += 1;     // Увеличиваем счёт
                timer = 0f;     // Сбрасываем таймер

                UpdateScoreText();

                // Проверяем и обновляем максимальный счёт
                if (score > maxScore)
                {
                    maxScore = score;
                    PlayerPrefs.SetInt("MaxScoreCosmos", maxScore); // Сохраняем максимальный счёт в PlayerPrefs
                    PlayerPrefs.Save(); // Явно сохраняем данные
                    UpdateMaxScoreText();
                }
            }
        }
    }

    public void StartCounting()
    {
        if (isPlayerAlive) // Запускаем счётчик только если игрок жив
        {
            isCounting = true;
        }
    }

    public void StopCounting()
    {
        isCounting = false;
        Debug.Log("Счётчик очков остановлен.");
    }

    // Метод, вызываемый при смерти игрока
    private void OnPlayerDeath()
    {
        isPlayerAlive = false; // Игрок мёртв
        StopCounting();        // Останавливаем счётчик очков
        Debug.Log("Счётчик очков остановлен из-за смерти игрока.");
    }

    // Обновляем текст текущего счёта
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }

    // Обновляем текст максимального счёта
    void UpdateMaxScoreText()
    {
        if (maxScoreText != null)
        {
            maxScoreText.text = $"Max: {maxScore}";
        }
    }
}