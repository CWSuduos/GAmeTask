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
            Debug.LogError("���������� ��������� ��� ���� � ����������!");
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
        // �������� ����� ��������� ������, �������� �� ��������
        int newIndex;
        do
        {
            newIndex = Random.Range(0, spriteObjectPairs.Length);
        } while (newIndex == currentPairIndex && spriteObjectPairs.Length > 1);

        currentPairIndex = newIndex;
        currentSprite = spriteObjectPairs[currentPairIndex].sprite;
        currentLinkedObject = spriteObjectPairs[currentPairIndex].linkedObject;

        // ��������� ������������ ������
        displayImage.sprite = currentSprite;

        // ������� ��� ������� ������� ���� �� ����� � ��������� ��
        UpdateAllLinkedObjects();

        Debug.Log($"������� ������ �� {currentSprite.name}, ��������� ������: {currentLinkedObject.name}");
    }

    void UpdateAllLinkedObjects()
    {
        // ������� ���������� ��������� � ���� �������� � ��������
        ResetAllObjects();

        // ������ ��������� ������� ������
        if (currentLinkedObject != null)
        {
            SpecialObject prefabSpecialObj = currentLinkedObject.GetComponent<SpecialObject>();
            if (prefabSpecialObj == null)
            {
                prefabSpecialObj = currentLinkedObject.AddComponent<SpecialObject>();
            }
            prefabSpecialObj.MakeSpecial();
            Debug.Log($"������ {currentLinkedObject.name} ���� ���������");
        }

        // ������� ��� ������� � ObjectCharacteristics �� �����
        ObjectCharacteristics[] allObjects = FindObjectsOfType<ObjectCharacteristics>();

        foreach (ObjectCharacteristics obj in allObjects)
        {
            // ���������, �������� �� ���� ������ ������� ��������� ��������
            if (obj.gameObject.name.Contains(currentLinkedObject.name))
            {
                // ������ ������ ���������
                SpecialObject specialObj = obj.GetComponent<SpecialObject>();
                if (specialObj == null)
                {
                    specialObj = obj.gameObject.AddComponent<SpecialObject>();
                }
                specialObj.MakeSpecial();

                // ��������� ������ �� ����������� ������ ������
                if (currentLinkedObject != null)
                {
                    ObjectCharacteristics prefabCharacteristics = currentLinkedObject.GetComponent<ObjectCharacteristics>();
                    if (prefabCharacteristics != null && prefabCharacteristics.specialDeathEffectPrefab != null)
                    {
                        obj.specialDeathEffectPrefab = prefabCharacteristics.specialDeathEffectPrefab;
                    }
                }

                Debug.Log($"������ {obj.gameObject.name} ���� ��������� � ������� ����������� ������ ������");
            }
        }
    }

    // ����� ����� ��� ������ ���� ��������
    void ResetAllObjects()
    {
        // ���������� ��� �������
        foreach (SpriteObjectPair pair in spriteObjectPairs)
        {
            if (pair.linkedObject != null)
            {
                SpecialObject prefabSpecialObj = pair.linkedObject.GetComponent<SpecialObject>();
                if (prefabSpecialObj != null)
                {
                    prefabSpecialObj.isSpecial = false;
                    Debug.Log($"�������� ��������� ������� {pair.linkedObject.name}");
                }
            }
        }

        // ���������� ��� ������� �� �����
        ObjectCharacteristics[] allObjects = FindObjectsOfType<ObjectCharacteristics>();
        foreach (ObjectCharacteristics obj in allObjects)
        {
            SpecialObject specialObj = obj.GetComponent<SpecialObject>();
            if (specialObj != null)
            {
                specialObj.isSpecial = false;
                Debug.Log($"�������� ��������� ������� {obj.gameObject.name}");
            }
        }
    }
    void LogCurrentSpriteAndObjectName(int spriteIndex)
    {
        if (currentSprite != null && currentSprite.texture != null)
        {
            Debug.Log($"������� �����������: {currentSprite.texture.name}, ��������� ������: {currentLinkedObject.name}");
        }
        else
        {
            Debug.Log($"������� �����������: Sprite {spriteIndex}, ��������� ������: {currentLinkedObject.name}");
        }
    }

    public void ActivateDeathSprite()
    {
        if (deathSprite != null)
        {
            isDeathState = true;
            displayImage.sprite = deathSprite;
            isTimerRunning = false;
            Debug.Log("����������� ������ ������");
        }
        else
        {
            Debug.LogWarning("������ ������ �� ��������!");
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