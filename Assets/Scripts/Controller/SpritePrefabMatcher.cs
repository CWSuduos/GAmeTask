using UnityEngine;
using UnityEngine.UI;

public class SpritePrefabMatcher : MonoBehaviour
{
    [Header("UI Elements")]
    public Image displayImage; // Поле для текущего изображения

    [Header("Sprites and Prefabs")]
    public Sprite[] sprites; // Массив спрайтов
    public GameObject[] prefabs; // Массив префабов, соответствующих спрайтам

    private Sprite currentSprite; // Текущий активный спрайт
    private bool isSpecial; // Флаг, указывающий, является ли спрайт особенным

    void Start()
    {
        if (displayImage == null)
        {
            Debug.LogError("Поле displayImage не назначено! Назначьте компонент Image в инспекторе.");
            return;
        }

        // Проверка данных массивов
        if (sprites.Length != prefabs.Length)
        {
            Debug.LogError("Количество спрайтов и префабов должно совпадать!");
            return;
        }

        // Инициализация текущего спрайта
        currentSprite = displayImage.sprite;

        if (currentSprite == null)
        {
            Debug.LogWarning("Начальный спрайт в displayImage равен null!");
        }

        CheckActiveSprite();
    }

    void Update()
    {
        
        // Проверяем, изменился ли текущий спрайт
        if (currentSprite != displayImage.sprite)
        {
            currentSprite = displayImage.sprite;
            CheckActiveSprite();
        }
    }

    // Метод для проверки текущего активного спрайта
    void CheckActiveSprite()
    {
        if (currentSprite == null)
        {
            Debug.LogWarning("currentSprite равен null! Пропускаем проверку.");
            return;
        }

        for (int i = 0; i < sprites.Length; i++)
        {
            // Проверяем, что элемент в массиве не равен null
            if (sprites[i] == null)
            {
                Debug.LogWarning($"sprites[{i}] равен null! Пропускаем этот элемент.");
                continue;
            }

            if (prefabs[i] == null)
            {
                Debug.LogWarning($"prefabs[{i}] равен null! Пропускаем этот элемент.");
                continue;
            }

            // Сравниваем текущий спрайт
            if (sprites[i] == currentSprite)
            {
                Debug.Log($"Текущее изображение: {currentSprite.name}, Связанный префаб: {prefabs[i].name}");

                // Определяем, является ли спрайт особенным
                isSpecial = IsSpriteSpecial(currentSprite);

                // Применяем особенное свойство к префабу
                ApplySpecialProperty(prefabs[i], isSpecial);

                return;
            }
        }

        Debug.LogWarning("Текущий спрайт не найден в массиве спрайтов!");
    }

    // Метод для определения, является ли спрайт особенным
    bool IsSpriteSpecial(Sprite sprite)
    {
        if (sprite == null)
        {
            Debug.LogWarning("Sprite равен null в методе IsSpriteSpecial!");
            return false;
        }

        // Проверка имени спрайта
        return sprite.name.Contains("Special");
    }

    // Метод для добавления "особенного свойства" префабу
    void ApplySpecialProperty(GameObject prefab, bool isSpecial)
    {
        if (prefab == null)
        {
            Debug.LogWarning("Prefab равен null в методе ApplySpecialProperty!");
            return;
        }

        Debug.Log($"Добавляем особенное свойство для префаба: {prefab.name}, Особенный: {isSpecial}");

        // Логика для начисления очков
        if (isSpecial)
        {
            Debug.Log("Очки начислены!");
            // Здесь можно вызвать метод для начисления очков, например:
            // ScoreManager.Instance.AddPoints(10);
        }
        else
        {
            Debug.Log("Очки не начислены.");
        }

        // Дополнительная логика (если требуется)
    }
}