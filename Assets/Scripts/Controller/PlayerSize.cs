using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSize : MonoBehaviour
{
    // �������� ������� ������
    private Vector3 originalScale;

    private void Start()
    {
        // ��������� �������� ������� ������
        originalScale = transform.localScale;
    }

    private void Update()
    {
        // ���������, �� ��������� �� ������� ������, � ���������� ��� � ���������
        if (transform.localScale != originalScale)
        {
            transform.localScale = originalScale;
        }
    }
}
