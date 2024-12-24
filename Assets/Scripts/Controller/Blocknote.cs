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
    private Dictionary<int, List<SpawnedObjectData>> pageData = new Dictionary<int, List<SpawnedObjectData>>(); // Данные объектов по страницам
    private List<GameObject> currentSpawnedObjects = new List<GameObject>(); // Объекты на текущей странице

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
        ClearCurrentObjects();
        currentPage++;
        UpdatePage();
    }

    private void PreviousPage()
    {
        if (currentPage > 1)
        {
            SaveCurrentPage();
            ClearCurrentObjects();
            currentPage--;
            UpdatePage();
        }
    }

    private void UpdatePage()
    {
        pageNumberText.text = $"Page:{currentPage}";

        if (pageData.ContainsKey(currentPage))
        {
            RestorePageObjects();
        }
        else
        {
            SpawnNewObjects();
        }
    }

    private void SpawnNewObjects()
    {
        ClearCurrentObjects();

        GameObject[] shuffledPrefabs = ShuffleArray(objectsToSpawn);

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (i < shuffledPrefabs.Length)
            {
                GameObject prefabToSpawn = shuffledPrefabs[i];
                GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPoints[i].transform.position, Quaternion.identity);
                spawnedObject.layer = LayerMask.NameToLayer(spawnLayer); // Установка слоя
                currentSpawnedObjects.Add(spawnedObject);
            }
        }
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
        if (!pageData.ContainsKey(currentPage))
        {
            pageData[currentPage] = new List<SpawnedObjectData>();
        }
        else
        {
            pageData[currentPage].Clear();
        }

        foreach (GameObject obj in currentSpawnedObjects)
        {
            if (obj != null)
            {
                SpawnedObjectData data = new SpawnedObjectData();
                data.prefabName = obj.name.Replace("(Clone)", "").Trim();
                data.position = obj.transform.position;
                data.rotation = obj.transform.rotation;
                pageData[currentPage].Add(data);
            }
        }
    }

    private void RestorePageObjects()
    {
        ClearCurrentObjects();

        if (pageData.TryGetValue(currentPage, out List<SpawnedObjectData> savedData))
        {
            foreach (SpawnedObjectData data in savedData)
            {
                GameObject prefab = FindPrefabByName(data.prefabName);
                if (prefab != null)
                {
                    GameObject restoredObject = Instantiate(prefab, data.position, data.rotation);
                    restoredObject.layer = LayerMask.NameToLayer(spawnLayer); //Установка слоя
                    currentSpawnedObjects.Add(restoredObject);
                }
            }
        }
    }

    private void ClearCurrentObjects()
    {
        foreach (GameObject obj in currentSpawnedObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        currentSpawnedObjects.Clear();
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

    [System.Serializable]
    public class SpawnedObjectData
    {
        public string prefabName;
        public Vector3 position;
        public Quaternion rotation;
    }
}