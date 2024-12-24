using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefabs : MonoBehaviour
{
    // ������ ��������, ������� ����� ��������
    public GameObject[] prefabsToSpawn;

    // ����������� ����� ����� ��������
    public float minTimeBetweenSpawns = 5f;

    // �������� ���������� ������� ����� ��������
    public float minRandomTime = 2f;
    public float maxRandomTime = 5f;

    private void Start()
    {
        // ������ �������� ��� ������ ��������
        StartCoroutine(SpawnPrefabsCoroutine());
    }

    private IEnumerator SpawnPrefabsCoroutine()
    {
        while (true)
        {
            // �������� ��������� ������ � ������� �������� �������, � �������� ��������� ���� ������
            Instantiate(prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)], transform.position, Quaternion.identity);

            // ��������� ������� 5 ������
            yield return new WaitForSeconds(minTimeBetweenSpawns);

            // ��������� ��������� ����� �� 2 �� 5 ������
            yield return new WaitForSeconds(Random.Range(minRandomTime, maxRandomTime));
        }
    }
}