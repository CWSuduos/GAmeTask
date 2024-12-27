using UnityEngine;

public class ButtonsData : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject loadingImage;
    [SerializeField] private GameObject sliderObject;
    [SerializeField] private GameObject buttonObject;
    [SerializeField] private TransitionManager transitionManager;

    public void OnButtonClick()
    {
        if (transitionManager != null)
        {
            transitionManager.LoadSceneWithAnimation(sceneName, loadingImage, sliderObject, buttonObject);
        }
        else
        {
            Debug.LogError("TransitionManager не задан у кнопки " + gameObject.name);
        }
    }
}