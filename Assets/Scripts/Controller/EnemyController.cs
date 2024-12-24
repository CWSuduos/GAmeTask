using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // ��������� �����
    public int damageAmount = 1; // ���������� �����, ���������� ��� ������������

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���������, ����������� �� � ������� (��� �������� � ����� "Player")
        if (collision.gameObject.CompareTag("Player"))
        {
            // �������� ��������� HealthManager � ������
            HealthManager playerHealth = collision.gameObject.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                // ������� ����
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

    // �������������� ����� ��� 3D-��� (���� ������������ 3D-������)
    private void OnCollisionEnter(Collision collision)
    {
        // ���������, ����������� �� � ������� (��� �������� � ����� "Player")
        if (collision.gameObject.CompareTag("Player"))
        {
            // �������� ��������� HealthManager � ������
            HealthManager playerHealth = collision.gameObject.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                // ������� ����
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