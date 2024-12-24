using UnityEngine;

public class ObjectTracker : MonoBehaviour
{
    // ������ �������, ������� ����� ������ �� ����� ������������ �������
    public GameObject replacementPrefab;

    // ����������� ������� �������
    private Vector3 initialPosition;

    void Start()
    {
        // ��������� ����������� ������� �������
        initialPosition = transform.position;

        Debug.Log($"����������� ������� ������� {gameObject.name}: {initialPosition}");
    }

    void Update()
    {
        // ���������, ������������ �� ������
        if (Vector3.Distance(transform.position, initialPosition) > 0.1f)
        {
            Debug.Log($"���������� ����������� ������� {gameObject.name}");

            // ������� ���������� ������ �� ����������� �������
            CreateReplacementObject();

            // ������� ���� ���������, ����� �� ��������� ������ �������
            Destroy(this);
        }
    }

    // ����� ��� �������� ����������� �������
    void CreateReplacementObject()
    {
        if (replacementPrefab != null)
        {
            // ������� ���������� ������ �� ����������� �������
            GameObject replacement = Instantiate(replacementPrefab, initialPosition, Quaternion.identity);

            // ������������� �������� ��� ����������� ������� (�����������)
            replacement.transform.SetParent(transform.parent, false);

            Debug.Log($"������ ���������� ������ {replacement.name} �� ������� {initialPosition}");
        }
        else
        {
            Debug.LogError("ReplacementPrefab �� �����!");
        }
    }
}