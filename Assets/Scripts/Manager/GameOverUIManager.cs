using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameOverUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject gameOverPanel; // Главная панель (можно использовать как контейнер)
    public Text finalScoreText;      // Текст для отображения финального счёта

    [Header("Spawner Settings")]
    public MonoBehaviour spawnerScript; // Поле для ссылки на скрипт спавнера
    public Transform spawnContainer;    // Родительский объект для спавненных объектов

    [Header("Objects to Hide and Show")]
    public GameObject[] objectsToHide; // Массив объектов, которые нужно скрыть
    public GameObject[] objectsToShow; // Массив объектов, которые нужно показать

    [Header("Win/Lose Panels")]
    public GameObject winPanel;   // Объект плашки победы
    public GameObject losePanel;  // Объект плашки поражения

    public bool isPlayerAlive = true; // Флаг, указывающий, жив ли игрок (для HandleTimerStop)

    private bool hasTimerStopped = false;

    private void HandleTimerStop()
    {
        hasTimerStopped = true;
        Debug.Log("Таймер остановился, флаг установлен.");
        if (isPlayerAlive)
        {
            ShowWinPanel();
        }
        else
        {
            ShowLosePanel();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DifficultyBasedTimer.OnTimerStop += HandleTimerStop;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        DifficultyBasedTimer.OnTimerStop -= HandleTimerStop;
    }

    private void Start()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
    }

    // Общий метод для показа панели и выполнения общих действий
    public void ShowResultPanel()
    {
        if (spawnerScript != null)
        {
            spawnerScript.enabled = false;
            Debug.Log("[GameOverUIManager] Скрипт спавнера был отключён.");
        }

        if (spawnContainer != null)
        {
            SetLayerForChildren(spawnContainer, "Background");
            Debug.Log("[GameOverUIManager] Все спавненные объекты перенесены на слой 'Background'.");
        }
        else
        {
            Debug.LogWarning("[GameOverUIManager] Контейнер спавна не назначен!");
        }

        HideObjects();
        ShowObjects();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = GetFinalScore().ToString();
        }
    }


    /// <summary>
    /// Показывает панель победы.
    /// </summary>
    public void ShowWinPanel()
    {
        ShowResultPanel(); // Выполняем общие действия
        if (winPanel != null) winPanel.SetActive(true);
        if (losePanel != null) losePanel.SetActive(false);
        Debug.Log("[GameOverUIManager] Показана панель победы.");
    }

    /// <summary>
    /// Показывает панель поражения.
    /// </summary>
    public void ShowLosePanel()
    {
        ShowResultPanel(); // Выполняем общие действия
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(true);
        Debug.Log("[GameOverUIManager] Показана панель поражения.");
    }

    private void HideObjects()
    {
        if (objectsToHide != null && objectsToHide.Length > 0)
        {
            foreach (GameObject obj in objectsToHide)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                    Debug.Log($"[GameOverUIManager] Объект '{obj.name}' был скрыт.");
                }
            }
        }
    }

    private void ShowObjects()
    {
        if (objectsToShow != null && objectsToShow.Length > 0)
        {
            foreach (GameObject obj in objectsToShow)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                    Debug.Log($"[GameOverUIManager] Объект '{obj.name}' был показан.");
                }
            }
        }
    }

    private int GetFinalScore()
    {
        return ScoreManager.Instance != null ? ScoreManager.Instance.GetCurrentScore() : 0;
    }

    private void SetLayerForChildren(Transform parent, string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);

        if (layer == -1)
        {
            Debug.LogError($"[GameOverUIManager] Слой '{layerName}' не существует! Проверьте название слоя.");
            return;
        }

        foreach (Transform child in parent)
        {
            child.gameObject.layer = layer;
            SetLayerForChildren(child, layerName);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
        Debug.Log("[GameOverUIManager] Панели закрыты при загрузке новой сцены.");
    }
}