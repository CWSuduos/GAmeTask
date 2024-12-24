using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitionButton : MonoBehaviour
{
    // ������ �� ������
    public Button transitionButton;

    // ��� �����, �� ������� ����� �������
    public string targetSceneName;

    void Start()
    {
        // ���������, ���� ������ ���������
        if (transitionButton != null)
        {
            // ��������� ���������� ������� ��� ������
            transitionButton.onClick.AddListener(OnTransitionButtonClick);
        }
        else
        {
            Debug.LogError("���������� ��������� ������ � ����������.");
        }
    }

    // �����, ������� ����������� ��� ������� �� ������
    void OnTransitionButtonClick()
    {
        // ���������, ��� ��� ����� ������
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            // ��������� ��������� �����
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("�� ������� ��� ����� ��� ��������.");
        }
    }
}