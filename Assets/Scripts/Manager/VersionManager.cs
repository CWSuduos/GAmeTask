using UnityEngine;
using UnityEngine.UI;
public class VersionManager : MonoBehaviour
{
    // ������ �� ������
    public Button versionButton;

    void Start()
    {
        // ���������, ���� ������ ���������
        if (versionButton != null)
        {
            // ��������� ���������� ������� ��� ������
            versionButton.onClick.AddListener(OnVersionButtonClick);
        }
        else
        {
            Debug.LogError("���������� ��������� ������ � ����������.");
        }
    }

    // �����, ������� ����������� ��� ������� �� ������
    void OnVersionButtonClick()
    {
        // �������� ������� ������ ����������
        string appVersion = Application.version;

        // ���������� ������ � ���� Toast-��������� (������ ��� Android)
        ShowToast("������ ����������: " + appVersion);
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
            AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", currentActivity, message, toastClass.GetStatic<int>("LENGTH_SHORT"));
            toastObject.Call("show");
        }));
#else
        // ��� ������ �������� (������� Unity Editor) ������ ������� ��������� � �������
        Debug.Log("������ ����������: " + message);
#endif
    }
}
