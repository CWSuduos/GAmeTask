using UnityEngine;
using UnityEngine.UI;

public class StatisticsManager : MonoBehaviour
{
    public static StatisticsManager Instance { get; private set; }

    private int deletedObjectsCount = 0;
    public Text deletedObjectsCountText; // ������ �� ��������� ���� ��� ����������� ��������

    private void Awake()
    {
        // ���������, ���� �� ��� ��������� �����������
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementDeletedObjectsCount()
    {
        deletedObjectsCount++;
        UpdateDeletedObjectsCountText();
    }

    private void UpdateDeletedObjectsCountText()
    {
        if (deletedObjectsCountText != null)
        {
            deletedObjectsCountText.text = "Score " + deletedObjectsCount;
        }
    }
    


}