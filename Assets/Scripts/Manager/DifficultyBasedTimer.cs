using UnityEngine;
using UnityEngine.UI;
using System;

public class DifficultyBasedTimer : MonoBehaviour
{
    public Text timerText; // ������ �� ��������� ���� ��� ����������� �������
    private float currentTime; // ������� ����� �������
    private bool isTimerRunning = false; // ����, ���������, �������� �� ������

    private void Start()
    {
        InitializeTimer(); // �������������� ������ ��� �������
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime; // ��������� ������� �����
            UpdateTimerText(); // ��������� ��������� ����

            if (currentTime <= 0) // ���� ����� �����������
            {
                currentTime = 0; // ������������� ����� �� 0
                isTimerRunning = false; // ������������� ������
                OnTimerFinished(); // �������� ���������� ���������� �������
            }
        }
    }

    public void InitializeTimer()
    {
        // �������� ������ ��������� �� DifficultyManager
        if (DifficultyManager.Instance != null)
        {
            var difficultyData = DifficultyManager.Instance.GetCurrentDifficultyData();
            if (difficultyData != null)
            {
                currentTime = difficultyData.time; // ������������� ����� �� ������ ���������
                Debug.Log($"[DifficultyBasedTimer] ������ ���������� ��: {currentTime} ������");
                UpdateTimerText(); // ��������� ��������� ����
                StartTimer(); // ��������� ������
            }
            else
            {
                Debug.LogError("[DifficultyBasedTimer] ������ ��������� �� �������!");
            }
        }
        else
        {
            Debug.LogError("[DifficultyBasedTimer] DifficultyManager �� ������!");
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            // ����������� ����� � ������� MM:SS
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            timerText.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
        }
        else
        {
            Debug.LogWarning("[DifficultyBasedTimer] ��������� ���� ��� ������� �� ���������!");
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
        Debug.Log("[DifficultyBasedTimer] ������ �������");
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        Debug.Log("[DifficultyBasedTimer] ������ ����������");
    }

    private void OnTimerFinished()
    {
        Debug.Log("[DifficultyBasedTimer] ������ ��������!");
        // ����� ����� �������� ������, ������� ����������� ����� ���������� �������
    }
}