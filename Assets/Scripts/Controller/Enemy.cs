using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 10; // Сколько урона наносит враг (задается в инспекторе)
    public Sprite deathSprite; // Спрайт, который будет показан при уничтожении врага

    private SpriteRenderer spriteRenderer; // Компонент для управления спрайтом
    private Collider2D enemyCollider; // Коллайдер врага
    private bool isDying = false; // Флаг для проверки, что враг уже умирает

    void Start()
    {
        // Получаем компоненты
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider2D>();

        if (spriteRenderer == null)
            Debug.LogWarning("SpriteRenderer не найден на враге!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDying) return; // Если враг уже умирает, ничего не делаем

        // Проверяем, что враг столкнулся с игроком
        PlayerHealthCosmos player = other.GetComponent<PlayerHealthCosmos>();
        if (player != null)
        {
            // Наносим урон игрокуs
            player.TakeDamage(damage);

            // Начинаем уничтожение врага
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        isDying = true; // Устанавливаем флаг
        Debug.Log("Враг умирает...");

        // Отключаем коллайдер, чтобы враг больше не взаимодействовал
        if (enemyCollider != null)
            enemyCollider.enabled = false;

        // Меняем спрайт врага на "спрайт смерти"
        if (deathSprite != null)
            spriteRenderer.sprite = deathSprite;

        // Останавливаем движение врага (если есть Rigidbody2D)
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.zero;

        // Ждем 1 секунду
        yield return new WaitForSeconds(1f);

        // Уничтожаем врага
        Destroy(gameObject);
    }
}