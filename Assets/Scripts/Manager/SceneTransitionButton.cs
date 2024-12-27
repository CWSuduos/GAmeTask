using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitionButton : MonoBehaviour
{
   
     public  void OnTransitionButtonClick(string targetSceneName)
    {
       SceneManager.LoadScene(targetSceneName);
       
    }
}