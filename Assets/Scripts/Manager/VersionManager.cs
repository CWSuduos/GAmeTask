using UnityEngine;
using UnityEngine.UI;
public class VersionManager : MonoBehaviour
{
    // Ссылка на кнопку
    public Button versionButton;

    void Start()
    {
        // Проверяем, если кнопка назначена
        if (versionButton != null)
        {
            // Добавляем обработчик события для кнопки
            versionButton.onClick.AddListener(OnVersionButtonClick);
        }
        else
        {
            Debug.LogError("Необходимо назначить кнопку в инспекторе.");
        }
    }

    // Метод, который выполняется при нажатии на кнопку
    void OnVersionButtonClick()
    {
        // Получаем текущую версию приложения
        string appVersion = Application.version;

        // Показываем версию в виде Toast-сообщения (только для Android)
        ShowToast("Версия приложения: " + appVersion);
    }

    // Метод для отображения Toast-сообщения на Android
    void ShowToast(string message)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        // Создаем Android-объект для Toast
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");

        // Показываем Toast
        currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", currentActivity, message, toastClass.GetStatic<int>("LENGTH_SHORT"));
            toastObject.Call("show");
        }));
#else
        // Для других платформ (включая Unity Editor) просто выводим сообщение в консоль
        Debug.Log("Версия приложения: " + message);
#endif
    }
}
