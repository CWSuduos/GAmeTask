using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class SceneLoader : MonoBehaviour
{

    [Header("UI Elements")]
    public Slider progressBar; // Ссылка на UI Slider

    [Header("Scene Settings")]
    public string nextSceneName; // Имя следующей сцены

    [Header("Loading Time Settings")]
    public float minLoadTime = 2f; // Минимальное время загрузки
    public float maxLoadTime = 5f; // Максимальное время загрузки

    private float totalLoadTime; // Итоговое случайное время загрузки
    private float currentProgress = 0f; // Текущее значение прогресса

    void Start()
    {
        // Проверка наличия прогресс-бара
        if (progressBar == null)
        {
            Debug.LogError("Progress Bar (Slider) не привязан к скрипту!");
            return;
        }

        // Сбрасываем прогресс
        progressBar.value = 0f;

        // Генерируем случайное общее время загрузки
        totalLoadTime = Random.Range(minLoadTime, maxLoadTime);

        // Запускаем корутину для заполнения прогресс-бара по этапам
        StartCoroutine(LoadProgressInStages());
    }

    IEnumerator LoadProgressInStages()
    {
        float elapsedTime = 0f; // Общее время, прошедшее с начала загрузки

        while (elapsedTime < totalLoadTime)
        {
            // Генерируем случайную скорость на текущем "этапе"
            float randomSpeed = Random.Range(0.1f, 0.5f);

            // Генерируем случайную длительность этапа
            float stageDuration = Random.Range(0.2f, 0.8f);

            float stageTime = 0f;

            // Заполняем прогресс на текущем этапе
            while (stageTime < stageDuration && elapsedTime < totalLoadTime)
            {
                float deltaProgress = randomSpeed * Time.deltaTime / totalLoadTime; // Добавляем прогресс
                currentProgress = Mathf.Clamp01(currentProgress + deltaProgress); // Ограничиваем от 0 до 1
                progressBar.value = currentProgress; // Обновляем прогресс-бар

                stageTime += Time.deltaTime;
                elapsedTime += Time.deltaTime;

                yield return null; // Ждем следующий кадр
            }
        }

        // Обеспечиваем заполнение прогресс-бара на 100%
        progressBar.value = 1f;

        // Переходим на следующую сцену
        LoadNextScene();
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Имя следующей сцены не указано!");
        }
    }
}
