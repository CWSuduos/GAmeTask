using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitionButton : MonoBehaviour
{
    // Ссылка на кнопку
    public Button transitionButton;

    // Имя сцены, на которую нужно перейти
    public string targetSceneName;

    void Start()
    {
        // Проверяем, если кнопка назначена
        if (transitionButton != null)
        {
            // Добавляем обработчик события для кнопки
            transitionButton.onClick.AddListener(OnTransitionButtonClick);
        }
        else
        {
            Debug.LogError("Необходимо назначить кнопку в инспекторе.");
        }
    }

    // Метод, который выполняется при нажатии на кнопку
    void OnTransitionButtonClick()
    {
        // Проверяем, что имя сцены задано
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            // Загружаем указанную сцену
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("Не указано имя сцены для перехода.");
        }
    }
}