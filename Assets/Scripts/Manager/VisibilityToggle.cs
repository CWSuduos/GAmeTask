using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class VisibilityToggle : MonoBehaviour
{
    public Button toggleButton; // ������ ��� ������������ ���������
    public List<GameObject> objectsToHide; // ������ �������� ��� �������
    public List<GameObject> objectsToShow; // ������ �������� ��� �����������
    private bool isHidden = false;  // ���� ��� ������������ ���������

    void Awake()
    {
        // ���������� ��������� ����� ������ �������� �����
        isHidden = false;
        UpdateVisibility(); // ������������� ��������� ���������
    }

    void Start()
    {
        if (toggleButton == null)
        {
            Debug.LogError("������ 'Toggle' �� ���������");
            return;
        }

        // ��������� ���������� ������
        toggleButton.onClick.AddListener(ToggleVisibility);

        // ����������, ��� ������� ����� ��������� ���������
        UpdateVisibility();
    }

    /// <summary>
    /// ����������� ��������� ��������.
    /// </summary>
    private void ToggleVisibility()
    {
        isHidden = !isHidden; // ����������� ���������
        UpdateVisibility();
    }

    /// <summary>
    /// ��������� ��������� �������� �� ������ �������� ���������.
    /// </summary>
    private void UpdateVisibility()
    {
        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
            {
                obj.SetActive(!isHidden); // �������� ������� �� ������
            }
            else
            {
                Debug.LogWarning("���� �� �������� � ������ objectsToHide �� ��������");
            }
        }

        foreach (GameObject obj in objectsToShow)
        {
            if (obj != null)
            {
                obj.SetActive(isHidden); // ���������� ������� �� ������
            }
            else
            {
                Debug.LogWarning("���� �� �������� � ������ objectsToShow �� ��������");
            }
        }
    }
}