using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float initialSpeed = 2f;
    public float acceleration = 0.1f;
    public float yBoundary = -10f;

    private float currentSpeed;
    private bool isStopped = false; // Флаг, указывающий, остановлен ли объект

    void Start()
    {
        currentSpeed = initialSpeed;
    }

    void Update()
    {
        if (!isStopped)
        {
            // Двигаем объект вниз, только если он не остановлен
            transform.Translate(Vector3.down * currentSpeed * Time.deltaTime);

            // Ускоряем объект
            currentSpeed += acceleration * Time.deltaTime;

            // Проверяем, достиг ли объект границы по оси Y
            if (transform.position.y <= yBoundary)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Проверяем, столкнулся ли объект с другим MovingObject или с любым другим объектом, если это необходимо
        if (collision.gameObject.GetComponent<MovingObject>() != null)
        {
            isStopped = true; // Останавливаем объект при столкновении
            // Здесь можно добавить дополнительные действия при столкновении, если нужно
        }
    }
}