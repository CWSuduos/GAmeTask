using UnityEngine;

public class ObjectGeneratorList : MonoBehaviour
{
    // Массив объектов, которые будут спавнены
    public GameObject[] objectsToSpawn;

    // Массив координат, по которым будут спавнены объекты
    public Vector3[] spawnPoints;

    // Флаг, который указывает, следует ли спавнить объекты случайно или по порядку
    public bool randomizeSpawnOrder = false;

    // Булевая переменная, которая указывает, следует ли заспавнить объекты снова
    public bool respawn = false;

    void Start()
    {
        // Проверяем, что массивы объектов и точек не пусты
        if (objectsToSpawn.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogError("Массивы объектов и точек не могут быть пустыми!");
            return;
        }

        // Спавним объекты по точкам
        SpawnObjects();
    }

    void SpawnObjects()
    {
        // Если флаг randomizeSpawnOrder установлен, то перемешиваем массив объектов
        if (randomizeSpawnOrder)
        {
            objectsToSpawn = ShuffleArray(objectsToSpawn);
        }

        // Спавним объекты по точкам
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            // Получаем текущую точку
            Vector3 point = spawnPoints[i];

            // Получаем текущий объект
            GameObject objectToSpawn = objectsToSpawn[i % objectsToSpawn.Length];

            // Спавним объект по текущей точке
            Instantiate(objectToSpawn, point, Quaternion.identity);
        }
    }

    // Метод, который перемешивает массив объектов
    GameObject[] ShuffleArray(GameObject[] array)
    {
        // Создаём новый массив объектов
        GameObject[] shuffledArray = new GameObject[array.Length];

        // Перемешиваем массив объектов
        for (int i = 0; i < array.Length; i++)
        {
            // Получаем случайный индекс
            int randomIndex = Random.Range(0, array.Length);

            // Помещаем объект по случайному индексу в новый массив
            shuffledArray[i] = array[randomIndex];

            // Удаляем объект из исходного массива, чтобы он не был повторен
            array = RemoveObjectFromArray(array, randomIndex);
        }

        // Возвращаем перемешанный массив объектов
        return shuffledArray;
    }

    // Метод, который удаляет объект из массива по индексу
    GameObject[] RemoveObjectFromArray(GameObject[] array, int index)
    {
        // Создаём новый массив объектов
        GameObject[] newArray = new GameObject[array.Length - 1];

        // Копируем объекты из исходного массива в новый массив, исключая объект по индексу
        for (int i = 0; i < index; i++)
        {
            newArray[i] = array[i];
        }

        for (int i = index + 1; i < array.Length; i++)
        {
            newArray[i - 1] = array[i];
        }

        // Возвращаем новый массив объектов
        return newArray;
    }

    void Update()
    {
        // Проверяем, следует ли заспавнить объекты снова
        if (respawn)
        {
            // Заспавним объекты снова
            SpawnObjects();
            // Сбрасываем флаг respawn
            respawn = false;
        }
    }
}