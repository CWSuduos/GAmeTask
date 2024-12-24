using UnityEngine;

public class ObjectTracker : MonoBehaviour
{
    // Префаб объекта, который будет создан на месте изначального объекта
    public GameObject replacementPrefab;

    // Изначальная позиция объекта
    private Vector3 initialPosition;

    void Start()
    {
        // Сохраняем изначальную позицию объекта
        initialPosition = transform.position;

        Debug.Log($"Изначальная позиция объекта {gameObject.name}: {initialPosition}");
    }

    void Update()
    {
        // Проверяем, переместился ли объект
        if (Vector3.Distance(transform.position, initialPosition) > 0.1f)
        {
            Debug.Log($"Обнаружено перемещение объекта {gameObject.name}");

            // Создаем заменяющий объект на изначальной позиции
            CreateReplacementObject();

            // Убираем этот компонент, чтобы не создавать лишние объекты
            Destroy(this);
        }
    }

    // Метод для создания заменяющего объекта
    void CreateReplacementObject()
    {
        if (replacementPrefab != null)
        {
            // Создаем заменяющий объект на изначальной позиции
            GameObject replacement = Instantiate(replacementPrefab, initialPosition, Quaternion.identity);

            // Устанавливаем родителя для заменяющего объекта (опционально)
            replacement.transform.SetParent(transform.parent, false);

            Debug.Log($"Создан заменяющий объект {replacement.name} на позиции {initialPosition}");
        }
        else
        {
            Debug.LogError("ReplacementPrefab не задан!");
        }
    }
}