using UnityEngine;

public class PairedObjectInteraction : MonoBehaviour
{
    [Tooltip("������ ������, ������� ����� ��������� ��� ��������. (������)")]
    public GameObject pairedObjectPrefab;

    [Tooltip("������, �� ������� ��������� ������������ ����� ��������������.")]
    public Sprite newSprite;

    private SpriteRenderer spriteRenderer;
    private Collider2D originalCollider;

    private string pairedTag = "pairedTag"; // ��� ������� �������

    // ���� ��� ������������ ��������� ���������
    private bool isStateChanged = false;
    private Sprite originalSprite; // ��������� ������������ ������

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalCollider = GetComponent<Collider2D>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer �� ������ � �������� ��������!");
        }
        if (originalCollider == null)
        {
            Debug.LogError("Collider2D �� ������ �� �������-���������!");
        }

        // ��������� ������������ ������
        originalSprite = spriteRenderer.sprite;

        // ���� ��� ����, ��������� ���
        if (gameObject.scene.rootCount != 0 && pairedObjectPrefab != null) // ���������� �������� �� ����
        {
            gameObject.tag = "OriginalTag";
            pairedObjectPrefab.tag = pairedTag;
        }

        // ��������������� ��������� ��� ��������� �������
        RestoreState();
    }

    private void OnEnable()
    {
        // ��������������� ��������� ��� ��������� �������
        RestoreState();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null || collision.gameObject == null)
        {
            Debug.LogError("Collision ��� GameObject ����� null!");
            return;
        }

        // ��������� ��� � ������
        if (collision.gameObject.CompareTag(pairedTag) &&
            gameObject.CompareTag("OriginalTag") &&
            collision.gameObject.name.StartsWith(pairedObjectPrefab.name)) // ���������� ��� ��� ������� ���������
        {
            ProcessInteraction(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null || other.gameObject == null)
        {
            Debug.LogError("Collider ��� GameObject ����� null!");
            return;
        }

        // ��������� ��� � ������
        if (other.gameObject.CompareTag(pairedTag) &&
            gameObject.CompareTag("OriginalTag") &&
            other.gameObject.name.StartsWith(pairedObjectPrefab.name)) // ���������� ��� ��� ������� ���������
        {
            ProcessInteraction(other.gameObject);
        }
    }

    private void ProcessInteraction(GameObject other)
    {
        if (other == null)
        {
            Debug.LogError("������ other ����� null!");
            return;
        }

        // ���������� ���� ������� �������
        Destroy(other);

        // ������ ������ ������������� �������
        if (newSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = newSprite;
        }
        else
        {
            if (newSprite == null)
            {
                Debug.LogWarning("NewSprite �� ��������!");
            }
            if (spriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer ����� null!");
            }
        }

        // ������� ��������� ������������� �������
        if (originalCollider != null)
        {
            Destroy(originalCollider);
        }
        else
        {
            Debug.LogError("OriginalCollider ����� null!");
        }

        // ������������� ���� ��������� ���������
        isStateChanged = true;
    }

    // ����� ��� �������������� ���������
    private void RestoreState()
    {
        if (isStateChanged)
        {
            // ���� ��������� ���� ��������, ��������� ���������
            if (newSprite != null && spriteRenderer != null)
            {
                spriteRenderer.sprite = newSprite;
            }
            // ������� ���������, ���� �� ��� ����������
            if (originalCollider != null)
            {
                Destroy(originalCollider);
            }
        }
        else
        {
            // ���� ��������� �� ���� ��������, ��������������� ������������ ������
            if (originalSprite != null && spriteRenderer != null)
            {
                spriteRenderer.sprite = originalSprite;
            }
        }
    }
}