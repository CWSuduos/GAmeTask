using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnList : MonoBehaviour
{
    // Массив объектов для спавна (задать через Inspector)
    public GameObject[] spawnableObjects;

    // Массив точек спавна (задать через Inspector)
    public Transform[] spawnPoints;

    // Список для отслеживания заспавнённых объектов
    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void Start()
    {
        // Изначальный спавн объектов
        SpawnObjects();

        // Запускаем процесс слежения за объектами
        StartCoroutine(CheckAndRespawn());
    }

    // Функция для спавна объектов
    private void SpawnObjects()
    {
        // Проверяем, есть ли точки спавна и объекты для спавна
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("Нет точек спавна! Пожалуйста, добавьте точки спавна в массив spawnPoints.");
            return;
        }

        if (spawnableObjects.Length == 0)
        {
            Debug.LogError("Нет объектов для спавна! Пожалуйста, добавьте объекты в массив spawnableObjects.");
            return;
        }

        // Очищаем список перед новым спавном
        spawnedObjects.Clear();

        // Спаун по каждой точке
        foreach (Transform spawnPoint in spawnPoints)
        {
            // Выбираем случайный объект из массива объектов
            GameObject randomObject = spawnableObjects[Random.Range(0, spawnableObjects.Length)];

            // Создаём объект на позиции точки спавна с нулевым поворотом
            GameObject spawned = Instantiate(randomObject, spawnPoint.position, Quaternion.identity);

            // Добавляем заспавнённый объект в список
            spawnedObjects.Add(spawned);
        }
    }

    // Корутин для проверки объектов и их респавна
    private IEnumerator CheckAndRespawn()
    {
        while (true) // Бесконечный цикл проверки
        {
            // Проверяем, уничтожены ли все объекты
            if (AllObjectsDestroyed())
            {
                Debug.Log("Все объекты уничтожены. Спавним новые объекты...");

                // Спаун всех новых объектов
                RespawnAllObjects();
            }

            // Проверяем объекты каждые 0.5 секунды
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Функция для проверки, уничтожены ли все объекты
    private bool AllObjectsDestroyed()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null) // Если хотя бы один объект существует
            {
                return false; // Не все объекты уничтожены
            }
        }
        return true; // Все объекты уничтожены
    }

    // Функция для спавна всех объектов заново
    private void RespawnAllObjects()
    {
        // Очищаем список на случай, если в нём остались старые ссылки
        spawnedObjects.Clear();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
           
            GameObject randomObject = spawnableObjects[Random.Range(0, spawnableObjects.Length)];

      
            GameObject newObject = Instantiate(randomObject, spawnPoints[i].position, Quaternion.identity);

            spawnedObjects.Add(newObject);
        }
    }
}