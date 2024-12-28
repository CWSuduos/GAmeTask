using UnityEngine;
using UnityEngine.UI;

public class DestroyedObjectsCounter : MonoBehaviour
{
    // Статическая переменная для хранения счёта уничтоженных объектов
    public static int destroyedObjects = 0;

    // Статическая переменная для хранения максимального счёта уничтоженных объектов
    public static int maxDestroyedObjects = 0;

    // Ссылка на текстовое поле для отображения счёта уничтоженных объектов
    public Text destroyedObjectsText;

    // Ссылка на текстовое поле для отображения максимального счёта уничтоженных объектов
    public Text maxDestroyedObjectsText;

    // Метод для увеличения счёта уничтоженных объектов
    public static void IncrementDestroyedObjects()
    {
        destroyedObjects++;
        if (destroyedObjects > maxDestroyedObjects)
        {
            maxDestroyedObjects = destroyedObjects;
        }
    }

    // Обновление текста в текстовых полях
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