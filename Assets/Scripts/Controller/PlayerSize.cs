using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSize : MonoBehaviour
{
    // Исходный масштаб игрока
    private Vector3 originalScale;

    private void Start()
    {
        // Сохраняем исходный масштаб игрока
        originalScale = transform.localScale;
    }

    private void Update()
    {
        // Проверяем, не изменился ли масштаб игрока, и возвращаем его к исходному
        if (transform.localScale != originalScale)
        {
            transform.localScale = originalScale;
        }
    }
}
