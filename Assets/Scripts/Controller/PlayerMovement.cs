using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float screenBoundary;

    public Button leftButton;
    public Button rightButton;

    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    private Vector3 originalScale;

    void Start()
    {
        screenBoundary = Camera.main.orthographicSize * Camera.main.aspect;

        // Добавляем обработчики событий для кнопок на телефоне
        AddPhoneButtonHandlers(leftButton, () => isMovingLeft = true, () => isMovingLeft = false);
        AddPhoneButtonHandlers(rightButton, () => isMovingRight = true, () => isMovingRight = false);

        originalScale = transform.localScale;
    }

    void AddPhoneButtonHandlers(Button button, UnityEngine.Events.UnityAction pointerDownAction, UnityEngine.Events.UnityAction pointerUpAction)
    {
        // Получаем или добавляем компонент EventTrigger
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = button.gameObject.AddComponent<EventTrigger>();

        // Настраиваем обработчик нажатия
        EventTrigger.Entry pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((eventData) => pointerDownAction());
        trigger.triggers.Add(pointerDown);

        // Настраиваем обработчик отпускания
        EventTrigger.Entry pointerUp = new EventTrigger.Entry();
        pointerUp.eventID = EventTriggerType.PointerUp;
        pointerUp.callback.AddListener((eventData) => pointerUpAction());
        trigger.triggers.Add(pointerUp);

        // Настраиваем обработчик выхода за пределы кнопки
        EventTrigger.Entry pointerExit = new EventTrigger.Entry();
        pointerExit.eventID = EventTriggerType.PointerExit;
        pointerExit.callback.AddListener((eventData) => pointerUpAction());
        trigger.triggers.Add(pointerExit);
    }

    void ResetScale()
    {
        transform.localScale = originalScale;
    }

    void CheckAndResetScale()
    {
        if (transform.localScale != originalScale)
        {
            ResetScale();
        }
    }

    void Update()
    {
        // Обработка ввода с клавиатуры
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            isMovingLeft = true;
            isMovingRight = false;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            isMovingRight = true;
            isMovingLeft = false;
        }
        else if (!Input.GetMouseButton(0)) // Если не нажата кнопка мыши
        {
            // Сбрасываем движение только если нет активных касаний
            if (Input.touchCount == 0)
            {
                isMovingLeft = false;
                isMovingRight = false;
            }
        }

        // Движение
        Vector3 newPosition = transform.position;

        if (isMovingLeft)
        {
            newPosition.x -= moveSpeed * Time.deltaTime;
        }

        if (isMovingRight)
        {
            newPosition.x += moveSpeed * Time.deltaTime;
        }

        newPosition.x = Mathf.Clamp(newPosition.x, -screenBoundary, screenBoundary);
        transform.position = newPosition;

        CheckAndResetScale();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ResetScale();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ResetScale();
    }

    // Публичные методы для вызова из UI кнопок при необходимости
    public void StartMovingLeft()
    {
        isMovingLeft = true;
        isMovingRight = false;
    }

    public void StartMovingRight()
    {
        isMovingRight = true;
        isMovingLeft = false;
    }

    public void StopMoving()
    {
        isMovingLeft = false;
        isMovingRight = false;
    }
}