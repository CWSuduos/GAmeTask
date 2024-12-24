using UnityEngine;
using UnityEngine.UI;

public class TermsOfUseButton : MonoBehaviour
{
    // ������ �� ������
    public Button termsButton;

    // ����� "Terms of Use"
    private string termsText = "By using this application, you agree to comply with and be bound by these terms. The application is provided 'as is,' and the developer reserves the right to modify or terminate the service without prior notice. You are responsible for your use of the application and any content you create or share. Unauthorized use, distribution, or reproduction of the application is strictly prohibited.";

    void Start()
    {
        // ���������, ���� ������ ���������
        if (termsButton != null)
        {
            // ��������� ���������� ������� ��� ������
            termsButton.onClick.AddListener(OnTermsButtonClick);
        }
        else
        {
            Debug.LogError("���������� ��������� ������ � ����������.");
        }
    }

    // �����, ������� ����������� ��� ������� �� ������
    void OnTermsButtonClick()
    {
        // ���������� ����� "Terms of Use" � ���� Toast-��������� (������ ��� Android)
        ShowToast(termsText);
    }

    // ����� ��� ����������� Toast-��������� �� Android
    void ShowToast(string message)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        // ������� Android-������ ��� Toast
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");

        // ���������� Toast
        currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", currentActivity, message, toastClass.GetStatic<int>("LENGTH_LONG"));
            toastObject.Call("show");
        }));
#else
        // ��� ������ �������� (������� Unity Editor) ������ ������� ��������� � �������
        Debug.Log("Terms of Use: " + message);
#endif
    }
}