
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
