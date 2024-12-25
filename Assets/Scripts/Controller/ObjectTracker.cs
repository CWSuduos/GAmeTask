using UnityEngine;

public class ObjectTracker : MonoBehaviour
{
    // Префаб объекта, который будет создан на месте изначального объекта
    public GameObject replacementPrefab;

    // Изначальная позиция объекта
    private Vector3 initialPosition;

    private GameObject replacementObject;

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
            replacementObject = Instantiate(replacementPrefab, initialPosition, Quaternion.identity);

            // Устанавливаем родителя для заменяющего объекта (опционально)
            replacementObject.transform.SetParent(transform.parent, false);

            Debug.Log($"Создан заменяющий объект {replacementObject.name} на позиции {initialPosition}");

            // Добавляем обработчик события для удаления рождённого объекта при удалении родителя
            replacementObject.AddComponent<DeleteObjectOnParentDestroy>();
        }
        else
        {
            Debug.LogError("ReplacementPrefab не задан!");
        }
    }
}

// Новый скрипт для удаления объекта при удалении его родителя
public class DeleteObjectOnParentDestroy : MonoBehaviour
{
    void OnDestroy()
    {
        // Если объект был удален, проверяем, был ли удален его родитель
        if (transform.parent == null)
        {
            // Удаляем объект
            Destroy(gameObject);
        }
    }

    // Обработчик события при удалении родительского объекта
    void OnParentDestroy()
    {
        // Удаляем объект
        Destroy(gameObject);
    }

    // Обработчик события при удалении родительского объекта
    void LateUpdate()
    {
        // Проверяем, был ли удален родительский объект
        if (transform.parent == null)
        {
            // Вызываем метод OnParentDestroy
            OnParentDestroy();
        }
    }
}