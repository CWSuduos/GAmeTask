using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class SceneLoader : MonoBehaviour
{

    [Header("UI Elements")]
    public Slider progressBar; // ������ �� UI Slider

    [Header("Scene Settings")]
    public string nextSceneName; // ��� ��������� �����

    [Header("Loading Time Settings")]
    public float minLoadTime = 2f; // ����������� ����� ��������
    public float maxLoadTime = 5f; // ������������ ����� ��������

    private float totalLoadTime; // �������� ��������� ����� ��������
    private float currentProgress = 0f; // ������� �������� ���������

    void Start()
    {
        // �������� ������� ��������-����
        if (progressBar == null)
        {
            Debug.LogError("Progress Bar (Slider) �� �������� � �������!");
            return;
        }

        // ���������� ��������
        progressBar.value = 0f;

        // ���������� ��������� ����� ����� ��������
        totalLoadTime = Random.Range(minLoadTime, maxLoadTime);

        // ��������� �������� ��� ���������� ��������-���� �� ������
        StartCoroutine(LoadProgressInStages());
    }

    IEnumerator LoadProgressInStages()
    {
        float elapsedTime = 0f; // ����� �����, ��������� � ������ ��������

        while (elapsedTime < totalLoadTime)
        {
            // ���������� ��������� �������� �� ������� "�����"
            float randomSpeed = Random.Range(0.1f, 0.5f);

            // ���������� ��������� ������������ �����
            float stageDuration = Random.Range(0.2f, 0.8f);

            float stageTime = 0f;

            // ��������� �������� �� ������� �����
            while (stageTime < stageDuration && elapsedTime < totalLoadTime)
            {
                float deltaProgress = randomSpeed * Time.deltaTime / totalLoadTime; // ��������� ��������
                currentProgress = Mathf.Clamp01(currentProgress + deltaProgress); // ������������ �� 0 �� 1
                progressBar.value = currentProgress; // ��������� ��������-���

                stageTime += Time.deltaTime;
                elapsedTime += Time.deltaTime;

                yield return null; // ���� ��������� ����
            }
        }

        // ������������ ���������� ��������-���� �� 100%
        progressBar.value = 1f;

        // ��������� �� ��������� �����
        LoadNextScene();
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("��� ��������� ����� �� �������!");
        }
    }
}
