using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameOverUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject gameOverPanel; // ������� ������ (����� ������������ ��� ���������)
    public Text finalScoreText;      // ����� ��� ����������� ���������� �����

    [Header("Spawner Settings")]
    public MonoBehaviour spawnerScript; // ���� ��� ������ �� ������ ��������
    public Transform spawnContainer;    // ������������ ������ ��� ���������� ��������

    [Header("Objects to Hide and Show")]
    public GameObject[] objectsToHide; // ������ ��������, ������� ����� ������
    public GameObject[] objectsToShow; // ������ ��������, ������� ����� ��������

    [Header("Win/Lose Panels")]
    public GameObject winPanel;   // ������ ������ ������
    public GameObject losePanel;  // ������ ������ ���������

    public bool isPlayerAlive = true; // ����, �����������, ��� �� ����� (��� HandleTimerStop)

    private bool hasTimerStopped = false;

    private void HandleTimerStop()
    {
        hasTimerStopped = true;
        Debug.Log("������ �����������, ���� ����������.");
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

    // ����� ����� ��� ������ ������ � ���������� ����� ��������
    public void ShowResultPanel()
    {
        if (spawnerScript != null)
        {
            spawnerScript.enabled = false;
            Debug.Log("[GameOverUIManager] ������ �������� ��� ��������.");
        }

        if (spawnContainer != null)
        {
            SetLayerForChildren(spawnContainer, "Background");
            Debug.Log("[GameOverUIManager] ��� ���������� ������� ���������� �� ���� 'Background'.");
        }
        else
        {
            Debug.LogWarning("[GameOverUIManager] ��������� ������ �� ��������!");
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
    /// ���������� ������ ������.
    /// </summary>
    public void ShowWinPanel()
    {
        ShowResultPanel(); // ��������� ����� ��������
        if (winPanel != null) winPanel.SetActive(true);
        if (losePanel != null) losePanel.SetActive(false);
        Debug.Log("[GameOverUIManager] �������� ������ ������.");
    }

    /// <summary>
    /// ���������� ������ ���������.
    /// </summary>
    public void ShowLosePanel()
    {
        ShowResultPanel(); // ��������� ����� ��������
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(true);
        Debug.Log("[GameOverUIManager] �������� ������ ���������.");
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
                    Debug.Log($"[GameOverUIManager] ������ '{obj.name}' ��� �����.");
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
                    Debug.Log($"[GameOverUIManager] ������ '{obj.name}' ��� �������.");
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
            Debug.LogError($"[GameOverUIManager] ���� '{layerName}' �� ����������! ��������� �������� ����.");
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
        Debug.Log("[GameOverUIManager] ������ ������� ��� �������� ����� �����.");
    }
}