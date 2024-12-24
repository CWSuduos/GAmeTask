using UnityEngine;
using UnityEngine.UI;

public class Example : MonoBehaviour
{
    void Start()
    {
        // �������� ��������� Image
        Image image = GetComponent<Image>();

        // ���������, ��� ��������� ������� ����� 10
        if (image.canvas.sortingOrder == 10)
        {
            // ��������� ��������� ������� �� 1
            image.canvas.sortingOrder = 9;
        }
    }
}