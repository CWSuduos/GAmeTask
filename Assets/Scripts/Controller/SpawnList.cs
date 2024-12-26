using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnList : MonoBehaviour
{
    // ������ �������� ��� ������ (������ ����� Inspector)
    public GameObject[] spawnableObjects;

    // ������ ����� ������ (������ ����� Inspector)
    public Transform[] spawnPoints;

    // ������ ��� ������������ ����������� ��������
    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void Start()
    {
        // ����������� ����� ��������
        SpawnObjects();

        // ��������� ������� �������� �� ���������
        StartCoroutine(CheckAndRespawn());
    }

    // ������� ��� ������ ��������
    private void SpawnObjects()
    {
        // ���������, ���� �� ����� ������ � ������� ��� ������
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("��� ����� ������! ����������, �������� ����� ������ � ������ spawnPoints.");
            return;
        }

        if (spawnableObjects.Length == 0)
        {
            Debug.LogError("��� �������� ��� ������! ����������, �������� ������� � ������ spawnableObjects.");
            return;
        }

        // ������� ������ ����� ����� �������
        spawnedObjects.Clear();

        // ����� �� ������ �����
        foreach (Transform spawnPoint in spawnPoints)
        {
            // �������� ��������� ������ �� ������� ��������
            GameObject randomObject = spawnableObjects[Random.Range(0, spawnableObjects.Length)];

            // ������ ������ �� ������� ����� ������ � ������� ���������
            GameObject spawned = Instantiate(randomObject, spawnPoint.position, Quaternion.identity);

            // ��������� ����������� ������ � ������
            spawnedObjects.Add(spawned);
        }
    }

    // ������� ��� �������� �������� � �� ��������
    private IEnumerator CheckAndRespawn()
    {
        while (true) // ����������� ���� ��������
        {
            // ���������, ���������� �� ��� �������
            if (AllObjectsDestroyed())
            {
                Debug.Log("��� ������� ����������. ������� ����� �������...");

                // ����� ���� ����� ��������
                RespawnAllObjects();
            }

            // ��������� ������� ������ 0.5 �������
            yield return new WaitForSeconds(0.5f);
        }
    }

    // ������� ��� ��������, ���������� �� ��� �������
    private bool AllObjectsDestroyed()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null) // ���� ���� �� ���� ������ ����������
            {
                return false; // �� ��� ������� ����������
            }
        }
        return true; // ��� ������� ����������
    }

    // ������� ��� ������ ���� �������� ������
    private void RespawnAllObjects()
    {
        // ������� ������ �� ������, ���� � �� �������� ������ ������
        spawnedObjects.Clear();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
           
            GameObject randomObject = spawnableObjects[Random.Range(0, spawnableObjects.Length)];

      
            GameObject newObject = Instantiate(randomObject, spawnPoints[i].position, Quaternion.identity);

            spawnedObjects.Add(newObject);
        }
    }
}