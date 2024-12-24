using UnityEngine;

public class PairObjectHandler : MonoBehaviour
{
    [Header("Ссылка на парный объект")]
    public GameObject pairObject; // Парный объект

    [Header("Новый спрайт после соприкосновения")]
    public Sprite newSprite; // Новый спрайт для оригинального объекта

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Получаем компонент SpriteRenderer оригинального объекта
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Проверка на null
        if (pairObject == null)
        {
            Debug.LogError("PairObject не назначен для " + gameObject.name);
        }
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer не найден на " + gameObject.name);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, что объект столкнулся с парным объектом
        if (collision.gameObject == pairObject)
        {
            // Уничтожаем парный объект
            Destroy(pairObject);

            // Меняем спрайт оригинального объекта
            if (newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }
            else
            {
                Debug.LogWarning("Новый спрайт не назначен для " + gameObject.name);
            }
        }
    }
}