using UnityEngine;
using UnityEngine.UI;
using System;

public class DifficultyBasedTimer : MonoBehaviour
{
    public Text timerText; // ������ �� ��������� ���� ��� ����������� �������
    private float currentTime; // ������� �����
    private bool isTimerRunning = false; // ����, ���������, �������� �� ������
    public static event Action OnTimerStop;

    private bool isInitialized = false; // ����, ����� InitializeTimer ��������� ������ ���� ��� � ������� �������

    // ��������� �������� ��� ������ ��������� �������
    public bool IsTimerRunning => isTimerRunning;

    private void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();

            if (currentTime <= 0)
            {
                currentTime = 0;
                isTimerRunning = false;
                OnTimerFinished();
                GameOverUIManager gameOverManager = FindObjectOfType<GameOverUIManager>();
                if (gameOverManager != null)
                {
                    gameOverManager.ShowWinPanel();
                    gameOverManager.ShowResultPanel();
                }
                else
                {
                    Debug.LogError("GameOverUIManager �� ������!");
                }
            }
        }
    }

    // ��������� ������ � ������� ����������
    public void StartTimerWithDifficulty()
    {
        if (!isInitialized && DifficultySettings.Instance != null)
        {
            var difficultyData = DifficultySettings.Instance.GetCurrentDifficultyData();
            if (difficultyData != null)
            {
                currentTime = difficultyData.time;
                Debug.Log($"[DifficultyBasedTimer] ������ ���������� ��: {currentTime} ������");
                UpdateTimerText();
                isTimerRunning = true; // ��������� ������
                isInitialized = true;  // ��������, ��� ������������� ���������
                Debug.Log("[DifficultyBasedTimer] ������ �������");
            }
            else
            {
                Debug.LogError("[DifficultyBasedTimer] ������ ��������� �� �������!");
            }
        }
        else if (isInitialized)
        {
            Debug.LogWarning("[DifficultyBasedTimer] ������ ��� ��� ���������������.");
        }
        else
        {
            Debug.LogError("[DifficultyBasedTimer] DifficultySettings �� ������!");
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            timerText.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
        }
        else
        {
            Debug.LogWarning("[DifficultyBasedTimer] ��������� ���� ��� ������� �� ���������!");
        }
    }

    // �����, ������� ����� ������� �� ������� ������
    public void OnStartButtonClicked()
    {
        StartTimerWithDifficulty();
    }

    // ��������� ����� ��� ��������� �������
    public void StopTimer()
    {
        isTimerRunning = false;
        Debug.Log("[DifficultyBasedTimer] ������ ����������");
    }

    private void OnTimerFinished()
    {
        Debug.Log("[DifficultyBasedTimer] ������ ��������!");
        OnTimerStop?.Invoke();
    }

    // ��������� ����� ��� ������ �������
    public void ResetTimer()
    {
        isTimerRunning = false;
        currentTime = 0f;
        UpdateTimerText();
        Debug.Log("[DifficultyBasedTimer] ������ �������.");
    }
}