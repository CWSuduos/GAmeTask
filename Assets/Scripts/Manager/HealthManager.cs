using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }

    [Header("Health Settings")]
    public int maxHealth = 3; // Максимальное количество здоровья
    private int currentHealth; // Текущее здоровье

    [Header("Prefabs")]
    public GameObject fullHeartPrefab; // Префаб полного сердца
    public GameObject emptyHeartPrefab; // Префаб пустого сердца

    [Header("Spawn Points")]
    public Transform[] heartSpawnPoints; // Точки для отображения сердец

    private GameObject[] currentHearts; // Массив созданных сердец

    private void Awake()
    {
        // Реализация Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeHealth();
    }

    private void InitializeHealth()
    {
        // Проверяем, что префабы и точки спавна назначены
        if (fullHeartPrefab == null || emptyHeartPrefab == null)
        {
            Debug.LogError("[HealthManager] Heart prefabs not assigned!");
            return;
        }

        if (heartSpawnPoints == null || heartSpawnPoints.Length == 0)
        {
            Debug.LogError("[HealthManager] Spawn points not assigned!");
            return;
        }

        currentHealth = maxHealth;
        currentHearts = new GameObject[maxHealth];

        // Создаём начальное отображение сердец
        UpdateHearts();
    }

    private void CreateHeart(int index, bool isFull)
    {
        if (index >= heartSpawnPoints.Length)
        {
            Debug.LogError($"[HealthManager] Invalid spawn point index: {index}");
            return;
        }

        // Удаляем старое сердце, если оно существует
        if (currentHearts[index] != null)
        {
            Destroy(currentHearts[index]);
        }

        // Создаём новое сердце
        GameObject prefab = isFull ? fullHeartPrefab : emptyHeartPrefab;
        Transform spawnPoint = heartSpawnPoints[index];

        currentHearts[index] = Instantiate(prefab, spawnPoint.position, Quaternion.identity, spawnPoint);
    }

    public void TakeDamage()
    {
        // Уменьшаем здоровье
        if (currentHealth > 0)
        {
            currentHealth--;
            UpdateHearts();
        }

        // Если здоровье закончилось
        if (currentHealth <= 0)
        {
            Debug.Log("[HealthManager] Game Over! Player health is zero.");
            PauseGame(); // Останавливаем игру
        }
    }

    private void UpdateHearts()
    {
        // Обновляем отображение сердец
        for (int i = 0; i < maxHealth; i++)
        {
            CreateHeart(i, i < currentHealth);
        }
    }

    public void ResetHealth()
    {
        // Сбрасываем здоровье
        currentHealth = maxHealth;
        UpdateHearts();
    }

    // Остановка игры
    public void PauseGame()
    {
        Debug.Log("[HealthManager] Game paused.");
        // Полностью останавливаем время
        FindObjectOfType<GameOverUIManager>()?.ShowGameOverPanel();
    }

    // Возобновление игры
    public void ResumeGame()
    {
        Debug.Log("[HealthManager] Game resumed.");
        Time.timeScale = 1f; // Возвращаем нормальное течение времени
    }

    // Для отладки текущего состояния
    public void DebugStatus()
    {
        Debug.Log($"[HealthManager] Current Health: {currentHealth}/{maxHealth}");
        Debug.Log($"Hearts array length: {(currentHearts != null ? currentHearts.Length : 0)}");
        Debug.Log($"Spawn points length: {(heartSpawnPoints != null ? heartSpawnPoints.Length : 0)}");
    }
}