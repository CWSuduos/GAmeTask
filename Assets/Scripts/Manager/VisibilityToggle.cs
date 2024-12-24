using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class VisibilityToggle : MonoBehaviour
{
    public Button toggleButton; // Кнопка для переключения видимости
    public List<GameObject> objectsToHide; // Список объектов для скрытия
    public List<GameObject> objectsToShow; // Список объектов для отображения
    private bool isHidden = false;  // Флаг для отслеживания состояния

    void Awake()
    {
        // Сбрасываем состояние перед каждым запуском сцены
        isHidden = false;
        UpdateVisibility(); // Устанавливаем начальную видимость
    }

    void Start()
    {
        if (toggleButton == null)
        {
            Debug.LogError("Кнопка 'Toggle' не назначена");
            return;
        }

        // Назначаем обработчик кнопки
        toggleButton.onClick.AddListener(ToggleVisibility);

        // Убеждаемся, что объекты имеют начальное состояние
        UpdateVisibility();
    }

    /// <summary>
    /// Переключает видимость объектов.
    /// </summary>
    private void ToggleVisibility()
    {
        isHidden = !isHidden; // Переключаем состояние
        UpdateVisibility();
    }

    /// <summary>
    /// Обновляет видимость объектов на основе текущего состояния.
    /// </summary>
    private void UpdateVisibility()
    {
        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
            {
                obj.SetActive(!isHidden); // Скрываем объекты из списка
            }
            else
            {
                Debug.LogWarning("Один из объектов в списке objectsToHide не назначен");
            }
        }

        foreach (GameObject obj in objectsToShow)
        {
            if (obj != null)
            {
                obj.SetActive(isHidden); // Показываем объекты из списка
            }
            else
            {
                Debug.LogWarning("Один из объектов в списке objectsToShow не назначен");
            }
        }
    }
}