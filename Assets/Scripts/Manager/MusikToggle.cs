using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    public AudioSource musicSource; // Ссылка на AudioSource с музыкой
    public Button toggleButton;     // Ссылка на кнопку
    private bool isMusicPlaying = true; // Флаг, указывает, играет ли музыка

    private void Start()
    {
        // Проверяем, что ссылки на компоненты назначены
        if (musicSource == null)
        {
            Debug.LogError("AudioSource не назначен! Добавьте ссылку на компонент AudioSource в инспекторе.");
            return;
        }

        if (toggleButton != null)
        {
            // Назначаем метод ToggleMusic на событие нажатия кнопки
            toggleButton.onClick.AddListener(ToggleMusic);
        }
        else
        {
            Debug.LogError("Button не назначен! Добавьте ссылку на кнопку в инспекторе.");
        }

        // Включаем повтор музыки
        musicSource.loop = true;
    }

    // Метод для включения/выключения музыки
    public void ToggleMusic()
    {
        if (isMusicPlaying)
        {
            musicSource.Pause(); // Останавливаем музыку
            isMusicPlaying = false;
            Debug.Log("Музыка выключена");
        }
        else
        {
            musicSource.Play(); // Включаем музыку
            isMusicPlaying = true;
            Debug.Log("Музыка включена");
        }
    }
}