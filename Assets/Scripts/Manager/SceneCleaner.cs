using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCleaner : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.scene.name == null) // Проверяем объекты из DontDestroyOnLoad
            {
                Debug.Log($"Удалён объект с DontDestroyOnLoad: {obj.name}");
                Destroy(obj); // Уничтожаем объект
            }
        }
    }
}