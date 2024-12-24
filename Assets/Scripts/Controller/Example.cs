using UnityEngine;
using UnityEngine.UI;

public class Example : MonoBehaviour
{
    void Start()
    {
        // Получаем компонент Image
        Image image = GetComponent<Image>();

        // Проверяем, что приоритет объекта равен 10
        if (image.canvas.sortingOrder == 10)
        {
            // Уменьшаем приоритет объекта на 1
            image.canvas.sortingOrder = 9;
        }
    }
}