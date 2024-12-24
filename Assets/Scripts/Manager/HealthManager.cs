using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }

    [Header("Health Settings")]
    public int maxHealth = 3; // ������������ ���������� ��������
    private int currentHealth; // ������� ��������

    [Header("Prefabs")]
    public GameObject fullHeartPrefab; // ������ ������� ������
    public GameObject emptyHeartPrefab; // ������ ������� ������

    [Header("Spawn Points")]
    public Transform[] heartSpawnPoints; // ����� ��� ����������� ������

    private GameObject[] currentHearts; // ������ ��������� ������

    private void Awake()
    {
        // ���������� Singleton
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
        // ���������, ��� ������� � ����� ������ ���������
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

        // ������ ��������� ����������� ������
        UpdateHearts();
    }

    private void CreateHeart(int index, bool isFull)
    {
        if (index >= heartSpawnPoints.Length)
        {
            Debug.LogError($"[HealthManager] Invalid spawn point index: {index}");
            return;
        }

        // ������� ������ ������, ���� ��� ����������
        if (currentHearts[index] != null)
        {
            Destroy(currentHearts[index]);
        }

        // ������ ����� ������
        GameObject prefab = isFull ? fullHeartPrefab : emptyHeartPrefab;
        Transform spawnPoint = heartSpawnPoints[index];

        currentHearts[index] = Instantiate(prefab, spawnPoint.position, Quaternion.identity, spawnPoint);
    }

    public void TakeDamage()
    {
        // ��������� ��������
        if (currentHealth > 0)
        {
            currentHealth--;
            UpdateHearts();
        }

        // ���� �������� �����������
        if (currentHealth <= 0)
        {
            Debug.Log("[HealthManager] Game Over! Player health is zero.");
            PauseGame(); // ������������� ����
        }
    }

    private void UpdateHearts()
    {
        // ��������� ����������� ������
        for (int i = 0; i < maxHealth; i++)
        {
            CreateHeart(i, i < currentHealth);
        }
    }

    public void ResetHealth()
    {
        // ���������� ��������
        currentHealth = maxHealth;
        UpdateHearts();
    }

    // ��������� ����
    public void PauseGame()
    {
        Debug.Log("[HealthManager] Game paused.");
        // ��������� ������������� �����
        FindObjectOfType<GameOverUIManager>()?.ShowGameOverPanel();
    }

    // ������������� ����
    public void ResumeGame()
    {
        Debug.Log("[HealthManager] Game resumed.");
        Time.timeScale = 1f; // ���������� ���������� ������� �������
    }

    // ��� ������� �������� ���������
    public void DebugStatus()
    {
        Debug.Log($"[HealthManager] Current Health: {currentHealth}/{maxHealth}");
        Debug.Log($"Hearts array length: {(currentHearts != null ? currentHearts.Length : 0)}");
        Debug.Log($"Spawn points length: {(heartSpawnPoints != null ? heartSpawnPoints.Length : 0)}");
    }
}