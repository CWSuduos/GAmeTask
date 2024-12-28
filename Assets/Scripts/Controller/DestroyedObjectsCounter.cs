using UnityEngine;
using UnityEngine.UI;

public class DestroyedObjectsCounter : MonoBehaviour
{
    // ����������� ���������� ��� �������� ����� ������������ ��������
    public static int destroyedObjects = 0;

    // ����������� ���������� ��� �������� ������������� ����� ������������ ��������
    public static int maxDestroyedObjects = 0;

    // ������ �� ��������� ���� ��� ����������� ����� ������������ ��������
    public Text destroyedObjectsText;

    // ������ �� ��������� ���� ��� ����������� ������������� ����� ������������ ��������
    public Text maxDestroyedObjectsText;

    // ����� ��� ���������� ����� ������������ ��������
    public static void IncrementDestroyedObjects()
    {
        destroyedObjects++;
        if (destroyedObjects > maxDestroyedObjects)
        {
            maxDestroyedObjects = destroyedObjects;
        }
    }

    // ���������� ������ � ��������� �����
    private void Update()
    {
        if (destroyedObjectsText != null)
        {
            destroyedObjectsText.text = "Score: " + destroyedObjects.ToString();
        }

        if (maxDestroyedObjectsText != null)
        {
            maxDestroyedObjectsText.text = "Max: " + maxDestroyedObjects.ToString();
        }
    }
}