using UnityEngine;

public class PairObjectHandler : MonoBehaviour
{
    [Header("������ �� ������ ������")]
    public GameObject pairObject; // ������ ������

    [Header("����� ������ ����� ���������������")]
    public Sprite newSprite; // ����� ������ ��� ������������� �������

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // �������� ��������� SpriteRenderer ������������� �������
        spriteRenderer = GetComponent<SpriteRenderer>();

        // �������� �� null
        if (pairObject == null)
        {
            Debug.LogError("PairObject �� �������� ��� " + gameObject.name);
        }
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer �� ������ �� " + gameObject.name);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���������, ��� ������ ���������� � ������ ��������
        if (collision.gameObject == pairObject)
        {
            // ���������� ������ ������
            Destroy(pairObject);

            // ������ ������ ������������� �������
            if (newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }
            else
            {
                Debug.LogWarning("����� ������ �� �������� ��� " + gameObject.name);
            }
        }
    }
}