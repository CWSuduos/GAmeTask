using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public Slider loadingSlider; // Ссылка на слайдер
    public string nextSceneName; // Имя следующей сцены
    public float sliderSpeed = 1f; // Скорость заполнения слайдера

    private bool isTransitioning = false;

    private void Start()
    {
        // Проверяем, назначен ли слайдер
        if (loadingSlider == null)
        {
            Debug.LogError("Ошибка: Слайдер не назначен в инспекторе!");
            return;
        }

        // Проверяем, задано ли имя следующей сцены
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError("Ошибка: Имя следующей сцены не задано!");
            return;
        }

        // Сбрасываем слайдер и флаг
        loadingSlider.value = 0f;
        isTransitioning = false;
    }

    private void Update()
    {
        if (isTransitioning || loadingSlider == null) return;

        // Заполняем слайдер
        loadingSlider.value += sliderSpeed * Time.deltaTime;

        // Если слайдер заполнился, переходим на следующую сцену
        if (loadingSlider.value >= 1f)
        {
            isTransitioning = true;
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Ошибка: Имя следующей сцены не указано!");
        }
    }
}