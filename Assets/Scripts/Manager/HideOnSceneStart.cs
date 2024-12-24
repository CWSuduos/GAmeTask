using UnityEngine;

public class HideOnSceneStart : MonoBehaviour
{
    private bool isHidden = true;
    
    void Start()
    {
        gameObject.SetActive(false); // �������� ������ ��� ������� �����
    }
    
    public void ShowObject()
    {
        gameObject.SetActive(true);
        isHidden = false;
        Debug.Log($"{gameObject.name} ��� �������.");
    }

    public void HideObject()
    {
        gameObject.SetActive(false);
        isHidden = true;
        Debug.Log($"{gameObject.name} ��� �����.");
    }
}