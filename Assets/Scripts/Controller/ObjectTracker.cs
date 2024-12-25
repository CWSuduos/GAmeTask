using UnityEngine;

public class ObjectTracker : MonoBehaviour
{
    // ������ �������, ������� ����� ������ �� ����� ������������ �������
    public GameObject replacementPrefab;

    // ����������� ������� �������
    private Vector3 initialPosition;

    private GameObject replacementObject;

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
            replacementObject = Instantiate(replacementPrefab, initialPosition, Quaternion.identity);

            // ������������� �������� ��� ����������� ������� (�����������)
            replacementObject.transform.SetParent(transform.parent, false);

            Debug.Log($"������ ���������� ������ {replacementObject.name} �� ������� {initialPosition}");

            // ��������� ���������� ������� ��� �������� ��������� ������� ��� �������� ��������
            replacementObject.AddComponent<DeleteObjectOnParentDestroy>();
        }
        else
        {
            Debug.LogError("ReplacementPrefab �� �����!");
        }
    }
}

// ����� ������ ��� �������� ������� ��� �������� ��� ��������
public class DeleteObjectOnParentDestroy : MonoBehaviour
{
    void OnDestroy()
    {
        // ���� ������ ��� ������, ���������, ��� �� ������ ��� ��������
        if (transform.parent == null)
        {
            // ������� ������
            Destroy(gameObject);
        }
    }

    // ���������� ������� ��� �������� ������������� �������
    void OnParentDestroy()
    {
        // ������� ������
        Destroy(gameObject);
    }

    // ���������� ������� ��� �������� ������������� �������
    void LateUpdate()
    {
        // ���������, ��� �� ������ ������������ ������
        if (transform.parent == null)
        {
            // �������� ����� OnParentDestroy
            OnParentDestroy();
        }
    }
}