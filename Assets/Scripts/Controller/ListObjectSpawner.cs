using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ListObjectSpawner : MonoBehaviour
{
    // ������ ������������ �������� (�������)
    public GameObject[] originalObjects;

    // ����� ������ (4 �����)
    public Transform[] spawnPoints;

    // ������ �������� ��������
    private List<GameObject> activeObjects = new List<GameObject>();

    void Start()
    {
        // �������� ������������ ������
        if (spawnPoints.Length != 4)
        {
            Debug.LogError("������������ ���������� ����� ������! ������ ���� 4.");
            return;
        }

        Debug.Log("�������� ����� ��������...");
        SpawnRandomObjects();
    }

    // ����� ��������� ��������
    void SpawnRandomObjects()
    {
        Debug.Log("�������� ����� ��������� ��������...");

        // ������� ������ �������, ���� ��� ����
        foreach (var obj in activeObjects)
        {
            Destroy(obj);
        }
        activeObjects.Clear();

        Debug.Log("������ ������� �������.");

        // �������� 4 ��������� �������
        var randomIndices = GetRandomIndices(4, originalObjects.Length);

        Debug.Log("������� ��������� �������: " + string.Join(", ", randomIndices));

        for (int i = 0; i < 4; i++)
        {
            int randomIndex = randomIndices[i];
            Debug.Log($"������� ������ {i} � �������� {randomIndex}...");

            // ���������� ������� ����� ������
            GameObject original = Instantiate(originalObjects[randomIndex], spawnPoints[i].position, Quaternion.identity);

            Debug.Log($"������ ��������: {original.name}, �������: {original.transform.position}");

            // ������������� �������� ��� ���������� ������� (����� ������)
            original.transform.SetParent(spawnPoints[i], false);

            Debug.Log($"���������� �������� {spawnPoints[i].name} ��� {original.name}");

            // ��������� � ������
            activeObjects.Add(original);

            Debug.Log($"�������� � ������: �������� {original.name}");
        }

        Debug.Log("����� �������� ��������.");
    }

    // ��������� ��������� �������� ��� ����������
    int[] GetRandomIndices(int count, int maxIndex)
    {
        var indices = Enumerable.Range(0, maxIndex).OrderBy(x => Random.value).Take(count).ToArray();
        return indices;
    }
}