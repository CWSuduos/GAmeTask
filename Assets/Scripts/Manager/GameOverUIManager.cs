using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverUIManager : MonoBehaviour
{
    
    [Header("UI Elements")]
    public GameObject gameOverPanel; // Панель Game Over
    public Text finalScoreText;      // Текст для отображения финального счёта

    [Header("Spawner Settings")]
    public MonoBehaviour spawnerScript; // Поле для ссылки на скрипт спавнера
    public Transform spawnContainer;    // Родительский объект для спавненных объектов

    [Header("Objects to Hide and Show")]
    public GameObject[] objectsToHide; // Массив объектов, которые нужно скрыть
    public GameObject[] objectsToShow; // Массив объектов, которые нужно показать

    private void OnEnable()
    {
        // Подписываемся на событие загрузки сцены
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Отписываемся от события загрузки сцены
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        // Скрываем панель при старте
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    /// <summary>
    /// Показывает финальную панель, останавливает спавнер, переносит объекты на слой ниже,
    /// скрывает одни объекты и делает видимыми другие.
    /// </summary>
    public void ShowGameOverPanel()
    {
        // Отключаем скрипт спавнера
        if (spawnerScript != null)
        {
            spawnerScript.enabled = false;
            Debug.Log("[GameOverUIManager] Скрипт спавнера был отключён.");
        }

        // Переносим все объекты спавна на слой ниже
        if (spawnContainer != null)
        {
            SetLayerForChildren(spawnContainer, "Background");
            Debug.Log("[GameOverUIManager] Все спавненные объекты перенесены на слой 'Background'.");
        }
        else
        {
            Debug.LogWarning("[GameOverUIManager] Контейнер спавна не назначен!");
        }

        // Скрываем объекты из массива objectsToHide
        HideObjects();

        // Делаем видимыми объекты из массива objectsToShow
        ShowObjects();
        
        // Показываем панель
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Устанавливаем финальный счёт
        if (finalScoreText != null)
        {
            finalScoreText.text = GetFinalScore().ToString();
        }
    }

    /// <summary>
    /// Скрывает все объекты из массива objectsToHide.
    /// </summary>
    private void HideObjects()
    {
        if (objectsToHide != null && objectsToHide.Length > 0)
        {
            foreach (GameObject obj in objectsToHide)
            {
                if (obj != null)
                {
                    obj.SetActive(false); // Делаем объект невидимым
                    Debug.Log($"[GameOverUIManager] Объект '{obj.name}' был скрыт.");
                }
            }
        }
    }

    /// <summary>
    /// Делает видимыми все объекты из массива objectsToShow.
    /// </summary>
    private void ShowObjects()
    {
        if (objectsToShow != null && objectsToShow.Length > 0)
        {
            foreach (GameObject obj in objectsToShow)
            {
                if (obj != null)
                {
                    obj.SetActive(true); // Делаем объект видимым
                    Debug.Log($"[GameOverUIManager] Объект '{obj.name}' был показан.");
                }
            }
        }
    }

    /// <summary>
    /// Получает финальный счёт из ScoreManager.
    /// </summary>
    private int GetFinalScore()
    {
        return ScoreManager.Instance != null ? ScoreManager.Instance.GetCurrentScore() : 0;
    }

    /// <summary>
    /// Рекурсивно устанавливает слой для всех дочерних объектов.
    /// </summary>
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
            child.gameObject.layer = layer; // Меняем слой текущего объекта
            SetLayerForChildren(child, layerName); // Рекурсивно для всех дочерних объектов
        }
    }
    
    /// <summary>
    /// Закрывает панель при загрузке новой сцены.
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
            Debug.Log("[GameOverUIManager] Панель Game Over закрыта при загрузке новой сцены.");
        }
    }
}