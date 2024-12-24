using UnityEngine;
using UnityEngine.UI;

public class TimerFruit : MonoBehaviour
{
    [Header("UI")]
    public Text timerText;
    public Image displayImage;

    [Header("Sprites and Linked Objects")]
    public SpriteObjectPair[] spriteObjectPairs;

    [Header("Death Sprite")]
    public Sprite deathSprite;

    private float timer = 30f;
    private bool isTimerRunning = true;
    private bool isDeathState = false;

    private Sprite currentSprite;
    private GameObject currentLinkedObject;
    private int currentPairIndex = -1;

    void Start()
    {
        if (timerText == null || displayImage == null || spriteObjectPairs.Length == 0)
        {
            Debug.LogError("Необходимо настроить все поля в инспекторе!");
            isTimerRunning = false;
            return;
        }

        ChangeRandomSpriteAndObject();
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (isTimerRunning && !isDeathState)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = 30f;
                ChangeRandomSpriteAndObject();
            }

            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        timerText.text = $"{timer:F1}";
    }

    void ChangeRandomSpriteAndObject()
    {
        // Выбираем новый случайный индекс, отличный от текущего
        int newIndex;
        do
        {
            newIndex = Random.Range(0, spriteObjectPairs.Length);
        } while (newIndex == currentPairIndex && spriteObjectPairs.Length > 1);

        currentPairIndex = newIndex;
        currentSprite = spriteObjectPairs[currentPairIndex].sprite;
        currentLinkedObject = spriteObjectPairs[currentPairIndex].linkedObject;

        // Обновляем отображаемый спрайт
        displayImage.sprite = currentSprite;

        // Находим все объекты нужного типа на сцене и обновляем их
        UpdateAllLinkedObjects();

        Debug.Log($"Изменен спрайт на {currentSprite.name}, связанный объект: {currentLinkedObject.name}");
    }

    void UpdateAllLinkedObjects()
    {
        // Сначала сбрасываем состояние у всех объектов и префабов
        ResetAllObjects();

        // Делаем особенным текущий префаб
        if (currentLinkedObject != null)
        {
            SpecialObject prefabSpecialObj = currentLinkedObject.GetComponent<SpecialObject>();
            if (prefabSpecialObj == null)
            {
                prefabSpecialObj = currentLinkedObject.AddComponent<SpecialObject>();
            }
            prefabSpecialObj.MakeSpecial();
            Debug.Log($"Префаб {currentLinkedObject.name} стал особенным");
        }

        // Находим все объекты с ObjectCharacteristics на сцене
        ObjectCharacteristics[] allObjects = FindObjectsOfType<ObjectCharacteristics>();

        foreach (ObjectCharacteristics obj in allObjects)
        {
            // Проверяем, является ли этот объект текущим связанным объектом
            if (obj.gameObject.name.Contains(currentLinkedObject.name))
            {
                // Делаем объект особенным
                SpecialObject specialObj = obj.GetComponent<SpecialObject>();
                if (specialObj == null)
                {
                    specialObj = obj.gameObject.AddComponent<SpecialObject>();
                }
                specialObj.MakeSpecial();

                // Обновляем ссылку на специальный префаб смерти
                if (currentLinkedObject != null)
                {
                    ObjectCharacteristics prefabCharacteristics = currentLinkedObject.GetComponent<ObjectCharacteristics>();
                    if (prefabCharacteristics != null && prefabCharacteristics.specialDeathEffectPrefab != null)
                    {
                        obj.specialDeathEffectPrefab = prefabCharacteristics.specialDeathEffectPrefab;
                    }
                }

                Debug.Log($"Объект {obj.gameObject.name} стал особенным и получил специальный эффект смерти");
            }
        }
    }

    // Новый метод для сброса всех объектов
    void ResetAllObjects()
    {
        // Сбрасываем все префабы
        foreach (SpriteObjectPair pair in spriteObjectPairs)
        {
            if (pair.linkedObject != null)
            {
                SpecialObject prefabSpecialObj = pair.linkedObject.GetComponent<SpecialObject>();
                if (prefabSpecialObj != null)
                {
                    prefabSpecialObj.isSpecial = false;
                    Debug.Log($"Сброшено состояние префаба {pair.linkedObject.name}");
                }
            }
        }

        // Сбрасываем все объекты на сцене
        ObjectCharacteristics[] allObjects = FindObjectsOfType<ObjectCharacteristics>();
        foreach (ObjectCharacteristics obj in allObjects)
        {
            SpecialObject specialObj = obj.GetComponent<SpecialObject>();
            if (specialObj != null)
            {
                specialObj.isSpecial = false;
                Debug.Log($"Сброшено состояние объекта {obj.gameObject.name}");
            }
        }
    }
    void LogCurrentSpriteAndObjectName(int spriteIndex)
    {
        if (currentSprite != null && currentSprite.texture != null)
        {
            Debug.Log($"Текущее изображение: {currentSprite.texture.name}, Связанный объект: {currentLinkedObject.name}");
        }
        else
        {
            Debug.Log($"Текущее изображение: Sprite {spriteIndex}, Связанный объект: {currentLinkedObject.name}");
        }
    }

    public void ActivateDeathSprite()
    {
        if (deathSprite != null)
        {
            isDeathState = true;
            displayImage.sprite = deathSprite;
            isTimerRunning = false;
            Debug.Log("Активирован спрайт смерти");
        }
        else
        {
            Debug.LogWarning("Спрайт смерти не назначен!");
        }
    }

    public void ResetDeathState()
    {
        isDeathState = false;
        isTimerRunning = true;
        ChangeRandomSpriteAndObject();
        timer = 30f;
    }
}

[System.Serializable]
public class SpriteObjectPair
{
    public Sprite sprite;
    public GameObject linkedObject;
}