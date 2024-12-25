using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectMonitor : MonoBehaviour
{
    // ��� ��� ������������
    public string targetTag = "pairedTag";

    // ���� ��� ���������� ������
    public Text scoreText;

    // ������ �������� � �������� �����
    private List<GameObject> trackedObjects = new List<GameObject>();

    // ������� �������� ��������
    private int removedObjectCount = 0;

    void Start()
    {
        // ����� ��� ������� � �������� ����� ��� ������
        RefreshTrackedObjects();
        UpdateScoreText();
    }

    void Update()
    {
        // ��������� ������ �� �������� �������
        CheckForDeletedObjects();
    }

    // ����� ��� ���������� ������ ������������� ��������
    private void RefreshTrackedObjects()
    {
        // �������� ������� ������
        trackedObjects.Clear();

        // ����� ��� ������� � ��������� ����� � �������� �� � ������
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(targetTag);
        trackedObjects.AddRange(objectsWithTag);
    }

    // �������� �� �������� �������
    private void CheckForDeletedObjects()
    {
        // ������� ��������� ������ ��� �������� ��������
        List<GameObject> removedObjects = new List<GameObject>();

        // ���������, ����� ������� ���� �������
        foreach (GameObject obj in trackedObjects)
        {
            if (obj == null) // ������ ��� �����
            {
                removedObjects.Add(obj);
            }
        }

        // ������� �� �� ������ � �������� �������
        foreach (GameObject removedObj in removedObjects)
        {
            trackedObjects.Remove(removedObj);
            removedObjectCount++; // ��������� �������
            UpdateScoreText();    // �������� ��������� ����
        }
    }

    // ����� ��� ���������� ������ �����
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + removedObjectCount;
        }
        else
        {
            Debug.LogWarning("���� scoreText �� ��������� � ����������.");
        }
    }
}