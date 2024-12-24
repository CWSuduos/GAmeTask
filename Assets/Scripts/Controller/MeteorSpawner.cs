using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeteorSpawner : MonoBehaviour
{
    [Header("������� ����������")]
    public GameObject smallMeteorPrefab;
    public GameObject mediumMeteorPrefab;
    public GameObject largeMeteorPrefab;

    [Header("Warning ������")]
    public GameObject warningTop;
    public GameObject warningRight;
    public GameObject warningLeft;

    [Header("��������� �������")]
    public float spawnInterval = 5f;     // �������� ����� ������� ����������
    public float warningDuration = 1f;   // ������������ ��������������

    private Queue<GameObject> meteorQueue = new Queue<GameObject>(); // ������� ��� ��������
    private const int MAX_METEORS = 100; // ������������ ���������� �������� �� �����

    private void Start()
    {
        // ������������ � �������� ��� �������������� � ������
        warningTop.SetActive(false);
        warningRight.SetActive(false);
        warningLeft.SetActive(false);

        StartCoroutine(SpawnMeteorites());
    }

    IEnumerator SpawnMeteorites()
    {
        while (true)
        {
            int warningType = Random.Range(0, 3);
            GameObject currentWarning = null;
            bool spawnLarge = Random.value < 0.1f; // 10% ���� �� ������� ��������

            switch (warningType)
            {
                case 0: // ����� ������
                    currentWarning = warningTop;
                    if (spawnLarge)
                        SpawnLargeMeteorFromTop();
                    else
                        SpawnClusterFromTop();
                    break;

                case 1: // ����� ������
                    currentWarning = warningRight;
                    SpawnFromSide(true, spawnLarge);
                    break;

                case 2: // ����� �����
                    currentWarning = warningLeft;
                    SpawnFromSide(false, spawnLarge);
                    break;
            }

            if (currentWarning != null)
            {
                currentWarning.SetActive(true);
                yield return new WaitForSeconds(warningDuration);
                currentWarning.SetActive(false);
            }

            yield return new WaitForSeconds(spawnInterval - warningDuration);
        }
    }

    void SpawnClusterFromTop()
    {
        float randomX = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect);
        Vector3 center = new Vector3(randomX, Camera.main.orthographicSize + 1, 0);
        Vector3 smallOffset = new Vector3(1, 0, 0);

        SpawnMeteor(smallMeteorPrefab, center - smallOffset, 7f);
        SpawnMeteor(mediumMeteorPrefab, center, 5f);
        SpawnMeteor(smallMeteorPrefab, center + smallOffset, 7f);
    }

    void SpawnLargeMeteorFromTop()
    {
        float randomX = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect);
        Vector3 center = new Vector3(randomX, Camera.main.orthographicSize + 1, 0);
        SpawnMeteor(largeMeteorPrefab, center, 3f);
    }

    void SpawnFromSide(bool fromRight, bool isLarge)
    {
        float minY = Camera.main.orthographicSize / 2;
        float maxY = Camera.main.orthographicSize;
        float yPos = Random.Range(minY, maxY);

        float xPos = fromRight
            ? Camera.main.orthographicSize * Camera.main.aspect + 1
            : -Camera.main.orthographicSize * Camera.main.aspect - 1;

        Vector3 spawnPos = new Vector3(xPos, yPos, 0);

        GameObject meteorPrefab = isLarge ? largeMeteorPrefab : mediumMeteorPrefab;
        float speed = isLarge ? 3f : 5f;

        GameObject meteor = Instantiate(meteorPrefab, spawnPos, Quaternion.identity);
        Vector3 direction = (new Vector3(fromRight ? -1 : 1, -1, 0)).normalized;
        meteor.GetComponent<Rigidbody2D>().velocity = direction * speed;

        AddMeteorToQueue(meteor);
    }

    void SpawnMeteor(GameObject prefab, Vector3 position, float speed)
    {
        GameObject meteor = Instantiate(prefab, position, Quaternion.identity);
        meteor.GetComponent<Rigidbody2D>().velocity = Vector3.down * speed;

        AddMeteorToQueue(meteor);
    }

    // ��������� ������ � ������� � ������� ������, ���� �� ������ 100
    void AddMeteorToQueue(GameObject meteor)
    {
        meteorQueue.Enqueue(meteor); // ��������� ������ � �������

        if (meteorQueue.Count > MAX_METEORS) // ���� �������� ������ ����������� ����������
        {
            GameObject oldestMeteor = meteorQueue.Dequeue(); // ������� ��������� ������ �� �������
            if (oldestMeteor != null)
            {
                Destroy(oldestMeteor); // ���������� ������ ������
            }
        }
    }
}