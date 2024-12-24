using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ListObjectSpawner : MonoBehaviour
{
    // Массив оригинальных объектов (префабы)
    public GameObject[] originalObjects;

    // Точки спавна (4 точки)
    public Transform[] spawnPoints;

    // Список активных объектов
    private List<GameObject> activeObjects = new List<GameObject>();

    void Start()
    {
        // Проверка корректности данных
        if (spawnPoints.Length != 4)
        {
            Debug.LogError("Неправильное количество точек спавна! Должно быть 4.");
            return;
        }

        Debug.Log("Начинаем спавн объектов...");
        SpawnRandomObjects();
    }

    // Спавн случайных объектов
    void SpawnRandomObjects()
    {
        Debug.Log("Начинаем спавн случайных объектов...");

        // Очищаем старые объекты, если они есть
        foreach (var obj in activeObjects)
        {
            Destroy(obj);
        }
        activeObjects.Clear();

        Debug.Log("Старые объекты очищены.");

        // Выбираем 4 случайных объекта
        var randomIndices = GetRandomIndices(4, originalObjects.Length);

        Debug.Log("Выбраны случайные индексы: " + string.Join(", ", randomIndices));

        for (int i = 0; i < 4; i++)
        {
            int randomIndex = randomIndices[i];
            Debug.Log($"Спавним объект {i} с индексом {randomIndex}...");

            // Используем позицию точки спавна
            GameObject original = Instantiate(originalObjects[randomIndex], spawnPoints[i].position, Quaternion.identity);

            Debug.Log($"Создан оригинал: {original.name}, позиция: {original.transform.position}");

            // Устанавливаем родителя для созданного объекта (точка спавна)
            original.transform.SetParent(spawnPoints[i], false);

            Debug.Log($"Установлен родитель {spawnPoints[i].name} для {original.name}");

            // Добавляем в список
            activeObjects.Add(original);

            Debug.Log($"Добавлен в список: оригинал {original.name}");
        }

        Debug.Log("Спавн объектов завершен.");
    }

    // Получение случайных индексов без повторений
    int[] GetRandomIndices(int count, int maxIndex)
    {
        var indices = Enumerable.Range(0, maxIndex).OrderBy(x => Random.value).Take(count).ToArray();
        return indices;
    }
}