using UnityEngine;

public class HideOnSceneStart : MonoBehaviour
{
    private bool isHidden = true;
    
    void Start()
    {
        gameObject.SetActive(false); // Скрываем объект при запуске сцены
    }
    
    public void ShowObject()
    {
        gameObject.SetActive(true);
        isHidden = false;
        Debug.Log($"{gameObject.name} был показан.");
    }

    public void HideObject()
    {
        gameObject.SetActive(false);
        isHidden = true;
        Debug.Log($"{gameObject.name} был скрыт.");
    }
}