using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 10; // ������� ����� ������� ���� (�������� � ����������)
    public Sprite deathSprite; // ������, ������� ����� ������� ��� ����������� �����

    private SpriteRenderer spriteRenderer; // ��������� ��� ���������� ��������
    private Collider2D enemyCollider; // ��������� �����
    private bool isDying = false; // ���� ��� ��������, ��� ���� ��� �������

    void Start()
    {
        // �������� ����������
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider2D>();

        if (spriteRenderer == null)
            Debug.LogWarning("SpriteRenderer �� ������ �� �����!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDying) return; // ���� ���� ��� �������, ������ �� ������

        // ���������, ��� ���� ���������� � �������
        PlayerHealthCosmos player = other.GetComponent<PlayerHealthCosmos>();
        if (player != null)
        {
            // ������� ���� ������s
            player.TakeDamage(damage);

            // �������� ����������� �����
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        isDying = true; // ������������� ����
        Debug.Log("���� �������...");

        // ��������� ���������, ����� ���� ������ �� ����������������
        if (enemyCollider != null)
            enemyCollider.enabled = false;

        // ������ ������ ����� �� "������ ������"
        if (deathSprite != null)
            spriteRenderer.sprite = deathSprite;

        // ������������� �������� ����� (���� ���� Rigidbody2D)
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.zero;

        // ���� 1 �������
        yield return new WaitForSeconds(1f);

        // ���������� �����
        Destroy(gameObject);
    }
}