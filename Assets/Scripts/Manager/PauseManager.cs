using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuPanel; // Reference to the pause menu panel
    public Button pauseButton;        // Reference to the PAUSE button
    public Button resumeButton;       // Reference to the RESUME button (inside the pause menu)

    private bool isPaused = false;

    void Start()
    {
        // Initially hide the pause menu and resume button
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Pause Menu Panel not assigned in the inspector!");
        }

        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(ResumeGame); // Directly connect resume button
            resumeButton.gameObject.SetActive(false); //Initially hide the resume button
        }
        else
        {
            Debug.LogError("Resume button not assigned in the inspector!");
        }

        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(PauseGame); // Pause button now only pauses
        }
        else
        {
            Debug.LogError("Pause button not assigned in the inspector!");
        }
    }

    void OnDestroy()
    {
        // Восстанавливаем время при уничтожении объекта
        Time.timeScale = 1f;
    }

    public void PauseGame() // Changed to public so other scripts could pause if needed.
    {
        if (!isPaused) // prevent pausing if already paused.
        {
            isPaused = true;
            Time.timeScale = 0f; // Stop the game time
            if (pauseMenuPanel != null)
            {
                pauseMenuPanel.SetActive(true); // Show the pause menu
                if (resumeButton != null)
                {
                    resumeButton.gameObject.SetActive(true); // Show resume button
                }
            }
            Debug.Log("Game Paused");
        }
    }

    public void ResumeGame()
    {
        if (isPaused) // prevent resuming if not paused
        {
            isPaused = false;
            Time.timeScale = 1f; // Resume game time
            if (pauseMenuPanel != null)
            {
                pauseMenuPanel.SetActive(false); // Hide the pause menu
                if (resumeButton != null)
                {
                    resumeButton.gameObject.SetActive(false); //Hide the resume button again
                }
            }
            Debug.Log("Game Resumed");
        }
    }

    void Update() // Still optional - Escape key handling  - Modified to only PAUSE
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
        }
    }
}