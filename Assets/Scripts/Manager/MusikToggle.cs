using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    public AudioSource musicSource; // ������ �� AudioSource � �������
    public Button toggleButton;     // ������ �� ������
    private bool isMusicPlaying = true; // ����, ���������, ������ �� ������

    private void Start()
    {
        // ���������, ��� ������ �� ���������� ���������
        if (musicSource == null)
        {
            Debug.LogError("AudioSource �� ��������! �������� ������ �� ��������� AudioSource � ����������.");
            return;
        }

        if (toggleButton != null)
        {
            // ��������� ����� ToggleMusic �� ������� ������� ������
            toggleButton.onClick.AddListener(ToggleMusic);
        }
        else
        {
            Debug.LogError("Button �� ��������! �������� ������ �� ������ � ����������.");
        }

        // �������� ������ ������
        musicSource.loop = true;
    }

    // ����� ��� ���������/���������� ������
    public void ToggleMusic()
    {
        if (isMusicPlaying)
        {
            musicSource.Pause(); // ������������� ������
            isMusicPlaying = false;
            Debug.Log("������ ���������");
        }
        else
        {
            musicSource.Play(); // �������� ������
            isMusicPlaying = true;
            Debug.Log("������ ��������");
        }
    }
}