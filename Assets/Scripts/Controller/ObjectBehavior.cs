using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{
    public bool isDestroyable = false;
    public bool isMovable = false;

    public int destroyClicksRequired = 1;
    private int currentClicks = 0;

    private bool isDragging = false;
    private Vector3 offset;

    private void Start()
    {
        if (isDestroyable)
        {
            ScoreCounter.Instance.TrackObject(gameObject);
        }
    }

    private void OnMouseDown()
    {
        if (isDestroyable)
        {
            currentClicks++;
            Debug.Log($"Кликов: {currentClicks} из {destroyClicksRequired}");
            if (currentClicks >= destroyClicksRequired)
            {
                Destroy(gameObject);
            }
        }
        else if (isMovable)
        {
            isDragging = true;
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseUp()
    {
        if (isMovable)
        {
            isDragging = false;
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

    private void Update()
    {
    }
}