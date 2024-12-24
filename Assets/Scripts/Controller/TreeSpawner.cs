using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefabs : MonoBehaviour
{
    // Массив префабов, которые будут спавнены
    public GameObject[] prefabsToSpawn;

    // Минимальное время между спавнами
    public float minTimeBetweenSpawns = 5f;

    // Диапазон случайного времени между спавнами
    public float minRandomTime = 2f;
    public float maxRandomTime = 5f;

    private void Start()
    {
        // Начать корутину для спавна префабов
        StartCoroutine(SpawnPrefabsCoroutine());
    }

    private IEnumerator SpawnPrefabsCoroutine()
    {
        while (true)
        {
            // Спавнить случайный префаб в позиции игрового объекта, к которому прикреплён этот скрипт
            Instantiate(prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)], transform.position, Quaternion.identity);

            // Подождать минимум 5 секунд
            yield return new WaitForSeconds(minTimeBetweenSpawns);

            // Подождать случайное время от 2 до 5 секунд
            yield return new WaitForSeconds(Random.Range(minRandomTime, maxRandomTime));
        }
    }
}