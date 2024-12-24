using UnityEngine;

public class ObjectGeneratorList : MonoBehaviour
{
    // ������ ��������, ������� ����� ��������
    public GameObject[] objectsToSpawn;

    // ������ ���������, �� ������� ����� �������� �������
    public Vector3[] spawnPoints;

    // ����, ������� ���������, ������� �� �������� ������� �������� ��� �� �������
    public bool randomizeSpawnOrder = false;

    // ������� ����������, ������� ���������, ������� �� ���������� ������� �����
    public bool respawn = false;

    void Start()
    {
        // ���������, ��� ������� �������� � ����� �� �����
        if (objectsToSpawn.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogError("������� �������� � ����� �� ����� ���� �������!");
            return;
        }

        // ������� ������� �� ������
        SpawnObjects();
    }

    void SpawnObjects()
    {
        // ���� ���� randomizeSpawnOrder ����������, �� ������������ ������ ��������
        if (randomizeSpawnOrder)
        {
            objectsToSpawn = ShuffleArray(objectsToSpawn);
        }

        // ������� ������� �� ������
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            // �������� ������� �����
            Vector3 point = spawnPoints[i];

            // �������� ������� ������
            GameObject objectToSpawn = objectsToSpawn[i % objectsToSpawn.Length];

            // ������� ������ �� ������� �����
            Instantiate(objectToSpawn, point, Quaternion.identity);
        }
    }

    // �����, ������� ������������ ������ ��������
    GameObject[] ShuffleArray(GameObject[] array)
    {
        // ������ ����� ������ ��������
        GameObject[] shuffledArray = new GameObject[array.Length];

        // ������������ ������ ��������
        for (int i = 0; i < array.Length; i++)
        {
            // �������� ��������� ������
            int randomIndex = Random.Range(0, array.Length);

            // �������� ������ �� ���������� ������� � ����� ������
            shuffledArray[i] = array[randomIndex];

            // ������� ������ �� ��������� �������, ����� �� �� ��� ��������
            array = RemoveObjectFromArray(array, randomIndex);
        }

        // ���������� ������������ ������ ��������
        return shuffledArray;
    }

    // �����, ������� ������� ������ �� ������� �� �������
    GameObject[] RemoveObjectFromArray(GameObject[] array, int index)
    {
        // ������ ����� ������ ��������
        GameObject[] newArray = new GameObject[array.Length - 1];

        // �������� ������� �� ��������� ������� � ����� ������, �������� ������ �� �������
        for (int i = 0; i < index; i++)
        {
            newArray[i] = array[i];
        }

        for (int i = index + 1; i < array.Length; i++)
        {
            newArray[i - 1] = array[i];
        }

        // ���������� ����� ������ ��������
        return newArray;
    }

    void Update()
    {
        // ���������, ������� �� ���������� ������� �����
        if (respawn)
        {
            // ��������� ������� �����
            SpawnObjects();
            // ���������� ���� respawn
            respawn = false;
        }
    }
}