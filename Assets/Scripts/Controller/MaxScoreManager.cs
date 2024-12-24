using UnityEngine;
using UnityEngine.UI;

public class MaxScoreManager : MonoBehaviour
{
    public static MaxScoreManager Instance { get; private set; }

    [Header("UI")]
    public Text maxScoreText; // ��������� ���� ��� ����������� ������������� �����
    public Text maxBonusScoreText; // ��������� ���� ��� ����������� ������������� ��������� �����

    private int maxScore = 0; // ������������ ����
    private int maxBonusScore = 0; // ������������ �������� ����

   
    private void Start()
    {
        LoadMaxScore();
        LoadMaxBonusScore();
        UpdateMaxScoreDisplay();
        UpdateMaxBonusScoreDisplay();
    }

    /// <summary>
    /// ���������� ������� ������������ ����.
    /// </summary>
    public int GetMaxScore()
    {
        return maxScore;
    }

    /// <summary>
    /// ��������� ������������ ���� � UI.
    /// </summary>
    public void UpdateMaxScore(int newScore)
    {
        if (newScore > maxScore)
        {
            maxScore = newScore;
            SaveMaxScore();
            UpdateMaxScoreDisplay();
            Debug.Log($"[MaxScoreManager] ����� ������������ ����: {maxScore}");
        }
    }

    /// <summary>
    /// ��������� ������������ ���� �� PlayerPrefs.
    /// </summary>
    private void LoadMaxScore()
    {
        maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        Debug.Log($"[MaxScoreManager] �������� ������������ ����: {maxScore}");
    }

    /// <summary>
    /// ��������� ������������ ���� � PlayerPrefs.
    /// </summary>
    private void SaveMaxScore()
    {
        PlayerPrefs.SetInt("MaxScore", maxScore);
        PlayerPrefs.Save();
        Debug.Log("[MaxScoreManager] ������������ ���� �������.");
    }

    /// <summary>
    /// ��������� ��������� ���� ��� ������������� �����.
    /// </summary>
    private void UpdateMaxScoreDisplay()
    {
        if (maxScoreText != null)
        {
            maxScoreText.text = $"{maxScore}";
        }
        else
        {
            Debug.LogWarning("[MaxScoreManager] ��������� ���� ��� ������������� ����� �� ���������!");
        }
    }

    /// <summary>
    /// ��������� ������������ �������� ���� � UI.
    /// </summary>
    public void UpdateMaxBonusScore(int newBonusScore)
    {
        if (newBonusScore > maxBonusScore)
        {
            maxBonusScore = newBonusScore;
            SaveMaxBonusScore();
            UpdateMaxBonusScoreDisplay();
            Debug.Log($"[MaxScoreManager] ����� ������������ �������� ����: {maxBonusScore}");
        }
    }

    /// <summary>
    /// ���������� ������� ������������ �������� ����.
    /// </summary>
    public int GetMaxBonusScore()
    {
        return maxBonusScore;
    }

    /// <summary>
    /// ��������� ������������ �������� ���� �� PlayerPrefs.
    /// </summary>
    private void LoadMaxBonusScore()
    {
        maxBonusScore = PlayerPrefs.GetInt("MaxBonusScore", 0);
        Debug.Log($"[MaxScoreManager] �������� ������������ �������� ����: {maxBonusScore}");
    }

    /// <summary>
    /// ��������� ������������ �������� ���� � PlayerPrefs.
    /// </summary>
    private void SaveMaxBonusScore()
    {
        PlayerPrefs.SetInt("MaxBonusScore", maxBonusScore);
        PlayerPrefs.Save();
        Debug.Log("[MaxScoreManager] ������������ �������� ���� �������.");
    }

    /// <summary>
    /// ��������� ��������� ���� ��� ������������� ��������� �����.
    /// </summary>
    private void UpdateMaxBonusScoreDisplay()
    {
        if (maxBonusScoreText != null)
        {
            maxBonusScoreText.text = $"{maxBonusScore}";
        }
        else
        {
            Debug.LogWarning("[MaxScoreManager] ��������� ���� ��� ������������� ��������� ����� �� ���������!");
        }
    }
}