using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectMonitor : MonoBehaviour
{
    // Тег для отслеживания
    public string targetTag = "pairedTag";

    // Поле для текстового вывода
    public Text scoreText;

    // Список объектов с заданным тегом
    private List<GameObject> trackedObjects = new List<GameObject>();

    // Счётчик удалённых объектов
    private int removedObjectCount = 0;

    void Start()
    {
        // Найти все объекты с заданным тегом при старте
        RefreshTrackedObjects();
        UpdateScoreText();
    }

    void Update()
    {
        // Проверять список на удалённые объекты
        CheckForDeletedObjects();
    }

    // Метод для обновления списка отслеживаемых объектов
    private void RefreshTrackedObjects()
    {
        // Очистить текущий список
        trackedObjects.Clear();

        // Найти все объекты с указанным тегом и добавить их в список
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(targetTag);
        trackedObjects.AddRange(objectsWithTag);
    }

    // Проверка на удалённые объекты
    private void CheckForDeletedObjects()
    {
        // Создать временный список для удалённых объектов
        List<GameObject> removedObjects = new List<GameObject>();

        // Проверить, какие объекты были удалены
        foreach (GameObject obj in trackedObjects)
        {
            if (obj == null) // Объект был удалён
            {
                removedObjects.Add(obj);
            }
        }

        // Удалить их из списка и обновить счётчик
        foreach (GameObject removedObj in removedObjects)
        {
            trackedObjects.Remove(removedObj);
            removedObjectCount++; // Увеличить счётчик
            UpdateScoreText();    // Обновить текстовое поле
        }
    }

    // Метод для обновления текста счёта
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + removedObjectCount;
        }
        else
        {
            Debug.LogWarning("Поле scoreText не назначено в инспекторе.");
        }
    }
}