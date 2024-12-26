using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public Slider loadingSlider; // ������ �� �������
    public string nextSceneName; // ��� ��������� �����
    public float sliderSpeed = 1f; // �������� ���������� ��������

    private bool isTransitioning = false;

    private void Start()
    {
        // ���������, �������� �� �������
        if (loadingSlider == null)
        {
            Debug.LogError("������: ������� �� �������� � ����������!");
            return;
        }

        // ���������, ������ �� ��� ��������� �����
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError("������: ��� ��������� ����� �� ������!");
            return;
        }

        // ���������� ������� � ����
        loadingSlider.value = 0f;
        isTransitioning = false;
    }

    private void Update()
    {
        if (isTransitioning || loadingSlider == null) return;

        // ��������� �������
        loadingSlider.value += sliderSpeed * Time.deltaTime;

        // ���� ������� ����������, ��������� �� ��������� �����
        if (loadingSlider.value >= 1f)
        {
            isTransitioning = true;
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("������: ��� ��������� ����� �� �������!");
        }
    }
}