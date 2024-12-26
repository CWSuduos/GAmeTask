using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShaperTimer : MonoBehaviour
{
    private float currentTime; // ������� ����� �������
    public Text timerText;     // ������ �� ��������� ������ UI ��� ����������� �������

   public void Start()
   {
       // �������� ����� �� ������� ��������� ���������
       if (DifficultySettings.Instance != null && DifficultySettings.Instance.StartTime)
       {
           currentTime = DifficultySettings.Instance.GetCurrentDifficultyData().time;
       }
       else
       {
           Debug.Log("DifficultySettings �� �������� ��� StartTime �� ����������!");
           currentTime = 120f; // �������� �� ���������
       }
  
       // ��������� �������� ��� ���������� ������� ������ �������
       StartCoroutine(UpdateTimer());
   }

    public IEnumerator UpdateTimer()
    {
        // ���� ������ ������ 0
       
        while (currentTime > 0)
        {
            // ��������� ������� ����� �� 1 �������
            currentTime--;

            // ��������� ����������� �������
            UpdateTimerText();

            // ��� 1 �������
            yield return new WaitForSeconds(1f);
        }

        // ����� ������ ��������� 0
        TimerFinished();
    }

    private void UpdateTimerText()
    {
        // ����������� ���������� ����� � ������ MM:SS
        int minutes = Mathf.FloorToInt(currentTime / 60); // ������
        int seconds = Mathf.FloorToInt(currentTime % 60); // �������

        // ��������� ��������� ������ UI
        if (timerText != null)
        {
            timerText.text = $"{minutes:00}:{seconds:00}"; // ������ MM:SS
        }
    }
    public GameOverUIManager gameOverUIManager;
    private void TimerFinished()
    {
        // ��������, ������� ����� ���������, ����� ������ ����� �� 0
        Debug.Log("������ ��������!");
        if (timerText != null)
        {
            gameOverUIManager.ShowLosePanel();
            gameOverUIManager.ShowResultPanel();
            timerText.text = "00:00"; // ������������� ����� �� 00:00
        }
    }
}