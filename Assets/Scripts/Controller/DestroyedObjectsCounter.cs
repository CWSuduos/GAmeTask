using UnityEngine;
using UnityEngine.UI; // Для стандартного Text

public class DestroyedObjectsCounter : MonoBehaviour
{
    // Ссылка на текстовое поле для отображения количества уничтоженных объектов
    public Text destroyedObjectsTextField;

    // Общее количество уничтоженных объектов
    private int destroyedObjectsCount = 0;

    void Start()
    {
        // Проверяем, что текстовое поле назначено
        if (destroyedObjectsTextField == null)
        {
            Debug.LogError("Текстовое поле не назначено! Убедитесь, что вы указали его в инспекторе.");
        }
        else
        {
            UpdateDestroyedObjectsText();
        }
    }

    // Метод для увеличения счётчика уничтоженных объектов
    public void IncrementDestroyedObjects()
    {
        destroyedObjectsCount++;
        UpdateDestroyedObjectsText();
    }

    // Метод для обновления текста
    private void UpdateDestroyedObjectsText()
    {
        if (destroyedObjectsTextField != null)
        {
            destroyedObjectsTextField.text = $"{destroyedObjectsCount}";
        }
    }
}