using UnityEngine;
using System.Collections.Generic;

public class DifficultySettings : MonoBehaviour
{
    public static DifficultySettings Instance { get; private set; }

    [System.Serializable]
    public class DifficultyData
    {
        public float time;   // ����� �������
        public float chance; // �������������� �������� (��������, ����������� �������)
    }

    [Header("��������� ���������")]
    public List<DifficultyData> difficultySettings = new List<DifficultyData>();
    public bool StartTime {  get; private set; }

    private int currentDifficultyIndex = 0;

    private void Awake()
    {
        // ��������� Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // ���� ������ ���������� ����, ��������� �������� �� ���������
        if (difficultySettings == null || difficultySettings.Count == 0)
        {
            difficultySettings = new List<DifficultyData>
            {
                new DifficultyData { time = 180f, chance = 0.2f },
                new DifficultyData { time = 240f, chance = 0.15f },
                new DifficultyData { time = 300f, chance = 0.1f }
            };
        }
        StartTime = false;
    }

    // ���������� ������ ��������� �� �������
    public DifficultyData GetDifficultyData(int difficultyIndex)
    {
        // ���������, ��� ������ ����������
        if (difficultyIndex >= 0 && difficultyIndex < difficultySettings.Count)
        {
            return difficultySettings[difficultyIndex];
        }
        else
        {
            Debug.LogWarning($"[DifficultySettings] ������������ ������ ���������: {difficultyIndex}");
            return null; // ���������� null, ���� ������ ������������
        }
    }

    // ���������� ������ ������� ���������
    public DifficultyData GetCurrentDifficultyData()
    {
        return GetDifficultyData(currentDifficultyIndex);
    }

    // ������������� ������� ���������
    public void SetDifficulty(int difficultyIndex)
    {
        if (difficultyIndex >= 0 && difficultyIndex < difficultySettings.Count)
        {
            currentDifficultyIndex = difficultyIndex;
            StartTime = true;
            Debug.Log($"[DifficultySettings] ����������� ���������: {difficultyIndex}, �����: {difficultySettings[difficultyIndex].time}");
        }
        else
        {
            Debug.LogWarning($"[DifficultySettings] ������������ ������ ���������: {difficultyIndex}");
        }
    }

}