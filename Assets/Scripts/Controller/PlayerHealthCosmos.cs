using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealthCosmos : MonoBehaviour
{
    public int health = 100; // Здоровье игрока
    public Text durabilityText; // Текстовое поле для отображения здоровья

    public static UnityEvent OnPlayerDeath = new UnityEvent(); // Событие "смерть игрока"

    private bool isDead = false; // Флаг для отслеживания смерти игрока

    void Update()
    {
        if (health <= 0 && !isDead)
        {
            isDead = true;
            OnPlayerDeath.Invoke(); // Вызываем событие
            Debug.Log("Игрок погиб!");
            Destroy(gameObject); // Уничтожаем игрока
            GameOverUIManager gameOverUIManager = GameObject.FindObjectOfType<GameOverUIManager>();
            gameOverUIManager.ShowGameOverPanel();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health < 0)
            health = 0;

        UpdateDurabilityText();
    }

    void UpdateDurabilityText()
    {
        if (durabilityText != null)
        {
            durabilityText.text = $"Durability: {health}%";
        }
    }
}