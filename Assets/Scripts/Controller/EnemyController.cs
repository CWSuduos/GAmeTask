using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Настройки урона
    public int damageAmount = 1; // Количество урона, наносимого при столкновении

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, столкнулись ли с игроком (или объектом с тегом "Player")
        if (collision.gameObject.CompareTag("Player"))
        {
            // Получаем компонент HealthManager у игрока
            HealthManager playerHealth = collision.gameObject.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                // Наносим урон
                for (int i = 0; i < damageAmount; i++)
                {
                    playerHealth.TakeDamage();
                }
            }
            else
            {
                Debug.LogWarning("Player does not have HealthManager component!");
            }
        }
    }

    // Альтернативный метод для 3D-игр (если используется 3D-физика)
    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, столкнулись ли с игроком (или объектом с тегом "Player")
        if (collision.gameObject.CompareTag("Player"))
        {
            // Получаем компонент HealthManager у игрока
            HealthManager playerHealth = collision.gameObject.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                // Наносим урон
                for (int i = 0; i < damageAmount; i++)
                {
                    playerHealth.TakeDamage();
                }
            }
            else
            {
                Debug.LogWarning("Player does not have HealthManager component!");
            }
        }
    }
}