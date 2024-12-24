using UnityEngine;
using UnityEngine.UI;
using System;

public class CountdownTimer : MonoBehaviour
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
            currentTime -= Time.deltaTime; // ��������� �����
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
        // �������� ������ ������� ��������� �� DifficultySettings
        if (DifficultySettings.Instance != null)
        {
            currentTime = DifficultySettings.Instance.GetCurrentDifficultyData().time;
            Debug.Log($"[CountdownTimer] ������ ���������� ��: {currentTime} ������");
            UpdateTimerText(); // ��������� ��������� ����
            StartTimer(); // ��������� ������
        }
        else
        {
            Debug.LogError("[CountdownTimer] DifficultySettings �� ������!");
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
            Debug.LogWarning("[CountdownTimer] ��������� ���� ��� ������� �� ���������!");
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
        Debug.Log("[CountdownTimer] ������ �������");
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        Debug.Log("[CountdownTimer] ������ ����������");
    }

    private void OnTimerFinished()
    {
        Debug.Log("[CountdownTimer] ������ ��������!");
        // ����� ����� �������� ������, ������� ����������� ����� ���������� �������
    }
}