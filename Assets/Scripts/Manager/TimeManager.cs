
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private void Awake()
    {
        
        PauseManager pauseManager = FindObjectOfType<PauseManager>();
         if (pauseManager == null)
        {
            Time.timeScale = 1f;
            Debug.Log("PauseManager not found. Time restored to normal.");
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
