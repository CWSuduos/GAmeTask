using UnityEngine;

public class LinkedObjectBehavior : MonoBehaviour
{
    public Animator animator; // �������� ��� ���������� ����������
    public GameObject transformedObjectPrefab; // ������, �� ������� ����� �������� ������ ����� ��������
    public string transformationAnimationTrigger = "Transform"; // ��� �������� ��� �������� ��������������

    private bool isTransforming = false; // ����, �����������, ��������� �� ������ � �������� ��������������

    // �����, ���������� ��� ��������� �������
    public void OnSpriteChanged(Sprite newSprite)
    {
        // ����� ����� �������� ������, ���� ����� ���-�� ������ ��� ��������� �������
    }

    // �����, ���������� ��� ��������������� � �������
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTransforming)
        {
            StartTransformation();
        }
    }

    // ����� ��� ������� �������� ��������������
    private void StartTransformation()
    {
        isTransforming = true;
        animator.SetTrigger(transformationAnimationTrigger);
    }

    // �����, ���������� �� ���������� �������� ��������������
    public void CompleteTransformation()
    {
        // ������� ����� ������ �� ����� �������
        Instantiate(transformedObjectPrefab, transform.position, transform.rotation);
        Destroy(gameObject); // ���������� ������ ������
    }
}