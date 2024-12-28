using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UI")]
    public Text scoreText; // ������ �� ��������� ������� UI ��� ����������� �����
    public Text maxScoreText; // ������ �� ����� ��� ����������� ������������� �����
    public Text scoreText1;
    private int currentScore = 0; // ������� ���� ������
    private int maxScore = 0;     // ������������ ����

    public event Action OnInitialized; // ������� ��� ������������� ScoreManager

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadMaxScore(); // ��������� ������������ ����
            InitializeScore();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ������������� ����� ��� ������.
    /// </summary>
    private void InitializeScore()
    {
        currentScore = 0;
        UpdateScoreDisplay();
        UpdateMaxScoreDisplay();
        Debug.Log("[ScoreManager] ���� ���������������: 0");
    }

    /// <summary>
    /// ��������� ���� ��� ����� ������� �������.
    /// </summary>
    /// <param name="points">���������� ����� ��� ����������.</param>
    /// <param name="objectName">�������� ������� (��� �������).</param>
    public void AddPoints(int points, string objectName = "")
    {
        currentScore += points;
        UpdateScoreDisplay();

        // ��������� � ��������� ������������ ����
        if (currentScore > maxScore)
        {
            maxScore = currentScore;
            SaveMaxScore();
            UpdateMaxScoreDisplay();
        }

        Debug.Log($"[ScoreManager] ����� ����: {currentScore}"); // ��� ��� ��������
    }

    /// <summary>
    /// ��������� ����������� �������� ����� �� UI.
    /// </summary>
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}";
            scoreText1.text = $"{currentScore}";
        }
        else
        {
            Debug.LogWarning("[ScoreManager] ��������� ���� ��� ����� �� ���������!");
        }
    }

    /// <summary>
    /// ��������� ����������� ������������� ����� �� UI.
    /// </summary>
    private void UpdateMaxScoreDisplay()
    {
        if (maxScoreText != null)
        {
            maxScoreText.text = $"{maxScore}";
        }
        else
        {
            Debug.LogWarning("[ScoreManager] ��������� ���� ��� ������������� ����� �� ���������!");
        }
    }

    /// <summary>
    /// ���������� ������� ���� ������.
    /// </summary>
    public int GetCurrentScore()
    {
        return currentScore;
    }

    /// <summary>
    /// ���������� ������������ ���� ������.
    /// </summary>
    public int GetMaxScore()
    {
        return maxScore;
    }

    /// <summary>
    /// ���������� ������� ���� �� ����.
    /// </summary>
    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreDisplay();
        Debug.Log("[ScoreManager] ���� ��� �������.");
    }

    /// <summary>
    /// ��������� ������������ ���� �� PlayerPrefs.
    /// </summary>
    private void LoadMaxScore()
    {
        maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        Debug.Log($"[ScoreManager] �������� ������������ ����: {maxScore}");
    }

    /// <summary>
    /// ��������� ������������ ���� � PlayerPrefs.
    /// </summary>
    private void SaveMaxScore()
    {
        PlayerPrefs.SetInt("MaxScore", maxScore);
        PlayerPrefs.Save();
        Debug.Log($"[ScoreManager] ����� ������������ ���� �������: {maxScore}");
    }
}