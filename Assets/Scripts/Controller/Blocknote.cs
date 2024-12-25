using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageObjectSpawner : MonoBehaviour
{
    [Header("Настройки объектов и спавна")]
    [SerializeField] private GameObject[] objectsToSpawn; // Массив префабов для спавна
    [SerializeField] private Transform[] spawnPoints;    // Точки спавна объектов
    [SerializeField] private string spawnLayer = "Default"; // Слой для спавна объектов

    [Header("UI Элементы")]
    [SerializeField] private Button nextPageButton;      // Кнопка "Следующая страница"
    [SerializeField] private Button previousPageButton;  // Кнопка "Предыдущая страница"
    [SerializeField] private Text pageNumberText;        // Текстовое поле с номером страницы

    private int currentPage = 1; // Текущий номер страницы
    private Dictionary<int, List<GameObject>> pageObjects = new Dictionary<int, List<GameObject>>(); // Объекты по страницам
    private List<GameObject> allSpawnedObjects = new List<GameObject>(); // Все объекты на сцене

    void Start()
    {
        // Настройка кнопок
        nextPageButton.onClick.AddListener(NextPage);
        previousPageButton.onClick.AddListener(PreviousPage);

        // Инициализация первой страницы
        UpdatePage();
    }

    private void NextPage()
    {
        SaveCurrentPage();
        HideCurrentObjects();
        currentPage++;
        UpdatePage();
    }

    private void PreviousPage()
    {
        if (currentPage > 1)
        {
            SaveCurrentPage();
            HideCurrentObjects();
            currentPage--;
            UpdatePage();
        }
    }

    private void UpdatePage()
    {
        pageNumberText.text = $"Page:{currentPage}";

        if (pageObjects.ContainsKey(currentPage))
        {
            ShowPageObjects();
        }
        else
        {
            SpawnNewObjects();
        }
    }

    private void SpawnNewObjects()
    {
        GameObject[] shuffledPrefabs = ShuffleArray(objectsToSpawn);
        List<GameObject> spawnedObjects = new List<GameObject>();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (i < shuffledPrefabs.Length)
            {
                GameObject prefabToSpawn = shuffledPrefabs[i];
                GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPoints[i].transform.position, Quaternion.identity);
                spawnedObject.layer = LayerMask.NameToLayer(spawnLayer); // Установка слоя
                spawnedObject.name = $"{spawnedObject.name}_{currentPage}_{i}"; // Индекс названия создания
                spawnedObjects.Add(spawnedObject);
                allSpawnedObjects.Add(spawnedObject);
            }
        }

        pageObjects[currentPage] = spawnedObjects;
    }

    private GameObject[] ShuffleArray(GameObject[] array)
    {
        GameObject[] shuffledArray = (GameObject[])array.Clone();
        for (int i = shuffledArray.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = shuffledArray[i];
            shuffledArray[i] = shuffledArray[randomIndex];
            shuffledArray[randomIndex] = temp;
        }
        return shuffledArray;
    }

    private void SaveCurrentPage()
    {
        if (pageObjects.ContainsKey(currentPage))
        {
            foreach (GameObject obj in pageObjects[currentPage])
            {
                if (obj != null && obj.activeSelf)
                {
                    obj.SetActive(false);
                }
            }
        }
    }

    private void ShowPageObjects()
    {
        if (pageObjects.TryGetValue(currentPage, out List<GameObject> savedObjects))
        {
            foreach (GameObject obj in savedObjects)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
        }
    }

    private void HideCurrentObjects()
    {
        if (pageObjects.ContainsKey(currentPage))
        {
            foreach (GameObject obj in pageObjects[currentPage])
            {
                if (obj != null && obj.activeSelf)
                {
                    obj.SetActive(false);
                }
            }
        }
    }

    private void HideAllObjects()
    {
        foreach (GameObject obj in allSpawnedObjects)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    private GameObject FindPrefabByName(string name)
    {
        foreach (GameObject prefab in objectsToSpawn)
        {
            if (prefab.name == name)
            {
                return prefab;
            }
        }
        return null;
    }
}