using UnityEngine;

public class PairedObjectInteraction : MonoBehaviour
{
    [Tooltip("Парный объект, который будет уничтожен при контакте. (Префаб)")]
    public GameObject pairedObjectPrefab;

    [Tooltip("Спрайт, на который заменится оригинальный после взаимодействия.")]
    public Sprite newSprite;

    private SpriteRenderer spriteRenderer;
    private Collider2D originalCollider;

    private string pairedTag = "pairedTag"; // Тег парного объекта

    // Флаг для отслеживания изменения состояния
    private bool isStateChanged = false;
    private Sprite originalSprite; // Сохраняем оригинальный спрайт

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalCollider = GetComponent<Collider2D>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer не найден в дочерних объектах!");
        }
        if (originalCollider == null)
        {
            Debug.LogError("Collider2D не найден на объекте-оригинале!");
        }

        // Сохраняем оригинальный спрайт
        originalSprite = spriteRenderer.sprite;

        // Если это клон, добавляем тег
        if (gameObject.scene.rootCount != 0 && pairedObjectPrefab != null) // Упрощенная проверка на клон
        {
            gameObject.tag = "OriginalTag";
            pairedObjectPrefab.tag = pairedTag;
        }

        // Восстанавливаем состояние при включении объекта
        RestoreState();
    }

    private void OnEnable()
    {
        // Восстанавливаем состояние при включении объекта
        RestoreState();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null || collision.gameObject == null)
        {
            Debug.LogError("Collision или GameObject равен null!");
            return;
        }

        // Проверяем тег и префаб
        if (collision.gameObject.CompareTag(pairedTag) &&
            gameObject.CompareTag("OriginalTag") &&
            collision.gameObject.name.StartsWith(pairedObjectPrefab.name)) // Убеждаемся что имя префаба совпадает
        {
            ProcessInteraction(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null || other.gameObject == null)
        {
            Debug.LogError("Collider или GameObject равен null!");
            return;
        }

        // Проверяем тег и префаб
        if (other.gameObject.CompareTag(pairedTag) &&
            gameObject.CompareTag("OriginalTag") &&
            other.gameObject.name.StartsWith(pairedObjectPrefab.name)) // Убеждаемся что имя префаба совпадает
        {
            ProcessInteraction(other.gameObject);
        }
    }

    private void ProcessInteraction(GameObject other)
    {
        if (other == null)
        {
            Debug.LogError("Объект other равен null!");
            return;
        }

        // Уничтожаем клон парного объекта
        Destroy(other);

        // Меняем спрайт оригинального объекта
        if (newSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = newSprite;
        }
        else
        {
            if (newSprite == null)
            {
                Debug.LogWarning("NewSprite не назначен!");
            }
            if (spriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer равен null!");
            }
        }

        // Удаляем коллайдер оригинального объекта
        if (originalCollider != null)
        {
            Destroy(originalCollider);
        }
        else
        {
            Debug.LogError("OriginalCollider равен null!");
        }

        // Устанавливаем флаг изменения состояния
        isStateChanged = true;
    }

    // Метод для восстановления состояния
    private void RestoreState()
    {
        if (isStateChanged)
        {
            // Если состояние было изменено, применяем изменения
            if (newSprite != null && spriteRenderer != null)
            {
                spriteRenderer.sprite = newSprite;
            }
            // Удаляем коллайдер, если он еще существует
            if (originalCollider != null)
            {
                Destroy(originalCollider);
            }
        }
        else
        {
            // Если состояние не было изменено, восстанавливаем оригинальный спрайт
            if (originalSprite != null && spriteRenderer != null)
            {
                spriteRenderer.sprite = originalSprite;
            }
        }
    }
}