using System;
using UnityEngine;
using UnityEngine.Events;
public class ObjectBehavior : MonoBehaviour
{
    public bool isDestroyable = false;
    public bool isMovable = false;

    public int destroyClicksRequired = 1;
    private int currentClicks = 0;

    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody2D rb2D;

    [Tooltip("����, �� ������� ������ ����������� ��� ������� �����.")]
    public string sortingLayerOnDrag = "Dragging";

    [Tooltip("���� �� ���������.")]
    public string defaultSortingLayer = "Default";

    private SpriteRenderer spriteRenderer;

    // �������, ������� ���������� ��� ����������� �������
    public delegate void OnDestroyEventHandler(GameObject destroyedObject);
    public event OnDestroyEventHandler onDestroy;
    private ObjectGeneratorList generator;

    public void SetGenerator(ObjectGeneratorList gen)
    {
        generator = gen;
    }
    private void Start()
    {
        if (isDestroyable)
        {
            ScoreCounter.Instance?.TrackObject(gameObject);
        }

        rb2D = GetComponent<Rigidbody2D>();
        if (rb2D == null)
        {
           
        }

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer �� ������ � �������� ��������!");
        }
    }

    private void OnMouseDown()
    {
        if (isDestroyable)
        {
            currentClicks++;
            Debug.Log($"������: {currentClicks} �� {destroyClicksRequired}");
            if (currentClicks >= destroyClicksRequired)
            {
                Destroy(gameObject);
            }
        }
        else if (isMovable)
        {
            isDragging = true;
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ������� Rigidbody2D
            if (rb2D != null)
            {
                Destroy(rb2D);
            }

            // ������ ���� ����������
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingLayerName = sortingLayerOnDrag;
            }
        }
    }

    private void OnMouseUp()
    {
        if (isMovable)
        {
            isDragging = false;

            // ��������� Rigidbody2D �������
            if (rb2D == null)
            {
                rb2D = gameObject.AddComponent<Rigidbody2D>();
                rb2D.bodyType = RigidbodyType2D.Dynamic;
                rb2D.gravityScale = 0;
            }

            // ���������� �������� ���� ����������
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingLayerName = defaultSortingLayer;
            }
        }
    }

    private void OnMouseDrag()
    {
        if (isMovable && isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            newPosition.z = 0;

            transform.position = newPosition;
        }
    }

    public event Action<GameObject> OnObjectDestroyed;
    private void OnDestroy()
    {
        
        if (gameObject.tag == "pairedTag")
        {
            // ���� ��, �� �������� ����� ObjectDestroyed � DestroyedObjectsCounter
            DestroyedObjectsCounter counter = FindObjectOfType<DestroyedObjectsCounter>();
            if (counter != null)
            {
                counter.ObjectDestroyed();
            }
        }
    }
}