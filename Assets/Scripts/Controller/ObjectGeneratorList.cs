using System.Collections.Generic;
using UnityEngine;

public class ObjectGeneratorList : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public Transform[] spawnPoints;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        if (!ValidateSetup()) return;
        SpawnObjects();
    }

    private bool ValidateSetup()
    {
        if (objectsToSpawn.Length == 0) { Debug.LogError("Objects to Spawn array is empty!", this); return false; }
        if (spawnPoints.Length != 4) { Debug.LogError("Spawn Points array must have exactly 4 elements!", this); return false; }
        foreach (var point in spawnPoints)
        {
            if (point == null) { Debug.LogError("One of the Spawn Points is null!", this); return false; }
        }
        return true;
    }

    private void SpawnObjects()
    {
        if (!ValidateSetup()) return;

        ClearSpawnedObjects();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            // �������� ��������� ������ ��� ������
            GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
            Vector3 spawnPosition = spawnPoints[i].position;
            // ������� ������ � ����� ������ ��� ��������
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            spawnedObjects.Add(spawnedObject);
        }
    }

    private void ClearSpawnedObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        spawnedObjects.Clear();
    }

    // ����� ��� ���������� ������ ����� �������� ���� ��������
    public void Respawn()
    {
        SpawnObjects();
    }

    // ���� ����� ����� ���� ������, ����� ��� ������� ������ ���� ������� � �������� ������
    public void OnAllObjectsDestroyed()
    {
        if (spawnedObjects.Count == 0)
        {
            Respawn();
        }
    }
}