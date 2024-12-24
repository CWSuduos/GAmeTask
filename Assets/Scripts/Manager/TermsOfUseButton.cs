using UnityEngine;
using UnityEngine.UI;

public class TermsOfUseButton : MonoBehaviour
{
    // Ссылка на кнопку
    public Button termsButton;

    // Текст "Terms of Use"
    private string termsText = "By using this application, you agree to comply with and be bound by these terms. The application is provided 'as is,' and the developer reserves the right to modify or terminate the service without prior notice. You are responsible for your use of the application and any content you create or share. Unauthorized use, distribution, or reproduction of the application is strictly prohibited.";

    void Start()
    {
        // Проверяем, если кнопка назначена
        if (termsButton != null)
        {
            // Добавляем обработчик события для кнопки
            termsButton.onClick.AddListener(OnTermsButtonClick);
        }
        else
        {
            Debug.LogError("Необходимо назначить кнопку в инспекторе.");
        }
    }

    // Метод, который выполняется при нажатии на кнопку
    void OnTermsButtonClick()
    {
        // Показываем текст "Terms of Use" в виде Toast-сообщения (только для Android)
        ShowToast(termsText);
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
            AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", currentActivity, message, toastClass.GetStatic<int>("LENGTH_LONG"));
            toastObject.Call("show");
        }));
#else
        // Для других платформ (включая Unity Editor) просто выводим сообщение в консоль
        Debug.Log("Terms of Use: " + message);
#endif
    }
}