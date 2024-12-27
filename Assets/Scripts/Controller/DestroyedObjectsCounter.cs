using UnityEngine;
using UnityEngine.UI; // ��� ������������ Text

public class DestroyedObjectsCounter : MonoBehaviour
{
    // ������ �� ��������� ���� ��� ����������� ���������� ������������ ��������
    public Text destroyedObjectsTextField;

    // ����� ���������� ������������ ��������
    private int destroyedObjectsCount = 0;

    void Start()
    {
        // ���������, ��� ��������� ���� ���������
        if (destroyedObjectsTextField == null)
        {
            Debug.LogError("��������� ���� �� ���������! ���������, ��� �� ������� ��� � ����������.");
        }
        else
        {
            UpdateDestroyedObjectsText();
        }
    }

    // ����� ��� ���������� �������� ������������ ��������
    public void IncrementDestroyedObjects()
    {
        destroyedObjectsCount++;
        UpdateDestroyedObjectsText();
    }

    // ����� ��� ���������� ������
    private void UpdateDestroyedObjectsText()
    {
        if (destroyedObjectsTextField != null)
        {
            destroyedObjectsTextField.text = $"{destroyedObjectsCount}";
        }
    }
}