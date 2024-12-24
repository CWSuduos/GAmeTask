using UnityEngine;

public class LinkedObjectBehavior : MonoBehaviour
{
    public Animator animator; // Аниматор для управления анимациями
    public GameObject transformedObjectPrefab; // Префаб, на который нужно заменить объект после анимации
    public string transformationAnimationTrigger = "Transform"; // Имя триггера для анимации перевоплощения

    private bool isTransforming = false; // Флаг, указывающий, находится ли объект в процессе перевоплощения

    // Метод, вызываемый при изменении спрайта
    public void OnSpriteChanged(Sprite newSprite)
    {
        // Здесь можно добавить логику, если нужно что-то делать при изменении спрайта
    }

    // Метод, вызываемый при соприкосновении с игроком
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTransforming)
        {
            StartTransformation();
        }
    }

    // Метод для запуска анимации перевоплощения
    private void StartTransformation()
    {
        isTransforming = true;
        animator.SetTrigger(transformationAnimationTrigger);
    }

    // Метод, вызываемый по завершении анимации перевоплощения
    public void CompleteTransformation()
    {
        // Создаем новый объект на месте старого
        Instantiate(transformedObjectPrefab, transform.position, transform.rotation);
        Destroy(gameObject); // Уничтожаем старый объект
    }
}