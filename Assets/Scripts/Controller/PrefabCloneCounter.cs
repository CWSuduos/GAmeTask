using System.Collections.Generic;
using UnityEngine;

public class PrefabCloneCounter : MonoBehaviour
{
    // Массив префабов, которые будут использоваться
    public GameObject[] prefabs;

    // Список для отслеживания созданных клонов
    private List<GameObject> spawnedClones = new List<GameObject>();

    // Количество удалённых клонов
    private int removedClonesCount = 0;

    void Start()
    {
        // Пример: создаём по одному клону каждого префаба
        foreach (GameObject prefab in prefabs)
        {
            if (prefab != null)
            {
                GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                spawnedClones.Add(clone);
            }
        }

        // Вызываем проверку каждую секунду
        InvokeRepeating("CheckForRemovedClones", 1f, 1f);
    }

    void CheckForRemovedClones()
    {
        // Удаляем из списка клоны, которые больше не существуют на сцене
        spawnedClones.RemoveAll(clone => clone == null);

        // Обновляем счётчик удалённых клонов
        removedClonesCount = prefabs.Length - spawnedClones.Count;

        // Выводим информацию в консоль
        Debug.Log($"Количество удалённых клонов: {removedClonesCount}");
    }

    // Метод для удаления случайного клона (для тестирования)
    public void RemoveRandomClone()
    {
        if (spawnedClones.Count > 0)
        {
            int randomIndex = Random.Range(0, spawnedClones.Count);
            GameObject cloneToRemove = spawnedClones[randomIndex];
            Destroy(cloneToRemove);
            Debug.Log($"Удалён клон: {cloneToRemove.name}");
        }
        else
        {
            Debug.Log("Нет клонов для удаления.");
        }
    }
}