using System.Collections.Generic;
using UnityEngine;

public class PrefabCloneCounter : MonoBehaviour
{
    // ������ ��������, ������� ����� ��������������
    public GameObject[] prefabs;

    // ������ ��� ������������ ��������� ������
    private List<GameObject> spawnedClones = new List<GameObject>();

    // ���������� �������� ������
    private int removedClonesCount = 0;

    void Start()
    {
        // ������: ������ �� ������ ����� ������� �������
        foreach (GameObject prefab in prefabs)
        {
            if (prefab != null)
            {
                GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                spawnedClones.Add(clone);
            }
        }

        // �������� �������� ������ �������
        InvokeRepeating("CheckForRemovedClones", 1f, 1f);
    }

    void CheckForRemovedClones()
    {
        // ������� �� ������ �����, ������� ������ �� ���������� �� �����
        spawnedClones.RemoveAll(clone => clone == null);

        // ��������� ������� �������� ������
        removedClonesCount = prefabs.Length - spawnedClones.Count;

        // ������� ���������� � �������
        Debug.Log($"���������� �������� ������: {removedClonesCount}");
    }

    // ����� ��� �������� ���������� ����� (��� ������������)
    public void RemoveRandomClone()
    {
        if (spawnedClones.Count > 0)
        {
            int randomIndex = Random.Range(0, spawnedClones.Count);
            GameObject cloneToRemove = spawnedClones[randomIndex];
            Destroy(cloneToRemove);
            Debug.Log($"����� ����: {cloneToRemove.name}");
        }
        else
        {
            Debug.Log("��� ������ ��� ��������.");
        }
    }
}