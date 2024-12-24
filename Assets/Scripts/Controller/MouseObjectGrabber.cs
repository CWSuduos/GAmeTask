using UnityEngine;

public class MouseObjectGrabber : MonoBehaviour
{
    // ������, ������� �� �����������
    private GameObject grabbedObject;

    // ��������� Rigidbody ������������ �������
    private Rigidbody grabbedRigidbody;

    // ��������� ������� ������� ������������ ������� ����
    private Vector3 initialMousePosition;

    // ��������� ������� �������
    private Vector3 initialObjectPosition;

    void Update()
    {
        // ���� ����� ������ ���� ������
        if (Input.GetMouseButtonDown(0))
        {
            TryGrabObject();
        }
        // ���� ����� ������ ���� ��������
        else if (Input.GetMouseButtonUp(0))
        {
            ReleaseObject();
        }
        // ���� ����� ������ ���� ������������
        else if (Input.GetMouseButton(0) && grabbedObject != null)
        {
            MoveObject();
        }
    }

    // �������� ��������� ������
    void TryGrabObject()
    {
        // ������� ��� �� ������ � �����, ���� ��������� ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // ���������, ���������� �� ��� �����-���� ������
        if (Physics.Raycast(ray, out hit))
        {
            // ���������, ���� �� � ������� Rigidbody
            grabbedRigidbody = hit.collider.GetComponent<Rigidbody>();
            if (grabbedRigidbody != null)
            {
                grabbedObject = hit.collider.gameObject;
                // ��������� ��������� ������� ���� � �������
                initialMousePosition = Input.mousePosition;
                initialObjectPosition = grabbedObject.transform.position;
                Debug.Log($"�������� ������: {grabbedObject.name}");
            }
        }
    }

    // ��������� ������
    void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            Debug.Log($"������ {grabbedObject.name} �������");
            grabbedObject = null;
            grabbedRigidbody = null;
        }
    }

    // ���������� ������
    void MoveObject()
    {
        // ��������� ������� ����� ������� � ��������� �������� ����
        Vector3 mouseDelta = Input.mousePosition - initialMousePosition;

        // ����������� ������� � ������� ����������
        Vector3 worldDelta = new Vector3(mouseDelta.x, mouseDelta.y, 0) / 100f; // ����� �� 100 ��� ���������������

        // ���������� ������ ������������ ��������� �������
        grabbedObject.transform.position = initialObjectPosition + worldDelta;
    }
}