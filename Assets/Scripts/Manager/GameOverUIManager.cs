using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverUIManager : MonoBehaviour
{
    
    [Header("UI Elements")]
    public GameObject gameOverPanel; // ������ Game Over
    public Text finalScoreText;      // ����� ��� ����������� ���������� �����

    [Header("Spawner Settings")]
    public MonoBehaviour spawnerScript; // ���� ��� ������ �� ������ ��������
    public Transform spawnContainer;    // ������������ ������ ��� ���������� ��������

    [Header("Objects to Hide and Show")]
    public GameObject[] objectsToHide; // ������ ��������, ������� ����� ������
    public GameObject[] objectsToShow; // ������ ��������, ������� ����� ��������

    private void OnEnable()
    {
        // ������������� �� ������� �������� �����
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // ������������ �� ������� �������� �����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        // �������� ������ ��� ������
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    /// <summary>
    /// ���������� ��������� ������, ������������� �������, ��������� ������� �� ���� ����,
    /// �������� ���� ������� � ������ �������� ������.
    /// </summary>
    public void ShowGameOverPanel()
    {
        // ��������� ������ ��������
        if (spawnerScript != null)
        {
            spawnerScript.enabled = false;
            Debug.Log("[GameOverUIManager] ������ �������� ��� ��������.");
        }

        // ��������� ��� ������� ������ �� ���� ����
        if (spawnContainer != null)
        {
            SetLayerForChildren(spawnContainer, "Background");
            Debug.Log("[GameOverUIManager] ��� ���������� ������� ���������� �� ���� 'Background'.");
        }
        else
        {
            Debug.LogWarning("[GameOverUIManager] ��������� ������ �� ��������!");
        }

        // �������� ������� �� ������� objectsToHide
        HideObjects();

        // ������ �������� ������� �� ������� objectsToShow
        ShowObjects();
        
        // ���������� ������
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // ������������� ��������� ����
        if (finalScoreText != null)
        {
            finalScoreText.text = GetFinalScore().ToString();
        }
    }

    /// <summary>
    /// �������� ��� ������� �� ������� objectsToHide.
    /// </summary>
    private void HideObjects()
    {
        if (objectsToHide != null && objectsToHide.Length > 0)
        {
            foreach (GameObject obj in objectsToHide)
            {
                if (obj != null)
                {
                    obj.SetActive(false); // ������ ������ ���������
                    Debug.Log($"[GameOverUIManager] ������ '{obj.name}' ��� �����.");
                }
            }
        }
    }

    /// <summary>
    /// ������ �������� ��� ������� �� ������� objectsToShow.
    /// </summary>
    private void ShowObjects()
    {
        if (objectsToShow != null && objectsToShow.Length > 0)
        {
            foreach (GameObject obj in objectsToShow)
            {
                if (obj != null)
                {
                    obj.SetActive(true); // ������ ������ �������
                    Debug.Log($"[GameOverUIManager] ������ '{obj.name}' ��� �������.");
                }
            }
        }
    }

    /// <summary>
    /// �������� ��������� ���� �� ScoreManager.
    /// </summary>
    private int GetFinalScore()
    {
        return ScoreManager.Instance != null ? ScoreManager.Instance.GetCurrentScore() : 0;
    }

    /// <summary>
    /// ���������� ������������� ���� ��� ���� �������� ��������.
    /// </summary>
    private void SetLayerForChildren(Transform parent, string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);

        if (layer == -1)
        {
            Debug.LogError($"[GameOverUIManager] ���� '{layerName}' �� ����������! ��������� �������� ����.");
            return;
        }

        foreach (Transform child in parent)
        {
            child.gameObject.layer = layer; // ������ ���� �������� �������
            SetLayerForChildren(child, layerName); // ���������� ��� ���� �������� ��������
        }
    }
    
    /// <summary>
    /// ��������� ������ ��� �������� ����� �����.
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
            Debug.Log("[GameOverUIManager] ������ Game Over ������� ��� �������� ����� �����.");
        }
    }
}