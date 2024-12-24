using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealthCosmos : MonoBehaviour
{
    public int health = 100; // �������� ������
    public Text durabilityText; // ��������� ���� ��� ����������� ��������

    public static UnityEvent OnPlayerDeath = new UnityEvent(); // ������� "������ ������"

    private bool isDead = false; // ���� ��� ������������ ������ ������

    void Update()
    {
        if (health <= 0 && !isDead)
        {
            isDead = true;
            OnPlayerDeath.Invoke(); // �������� �������
            Debug.Log("����� �����!");
            Destroy(gameObject); // ���������� ������
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