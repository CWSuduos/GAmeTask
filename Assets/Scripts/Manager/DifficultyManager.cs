using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    [Header("UI Components")]
    public Sprite[] sprites; // ������ �������� ��� ������ ���������
    public Image imageComponent; // UI-������� ��� ����������� ������� ���������
    public Button nextButton; // ������ "��������� ���������"
    public Button previousButton; // ������ "���������� ���������"
    public Button confirmButton; // ������ "����������� ���������"

    [Header("Settings")]
    public float delayBetweenClicks = 0.2f; // �������� ����� �������������� ����������
    public DifficultySettings difficultySettings; // ������ �� ��������� ���������

    private int currentSpriteIndex = 0; // ������� ������ ��������� ���������
    private bool isChanging = false; // ����, ��������������� ���� ������

    public int CurrentSpriteIndex { get { return currentSpriteIndex; } }
    public DifficultySettings.DifficultyData CurrentDifficultyData { get; private set; } // ������� ���������

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeDefaultDifficulty();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeDefaultDifficulty()
    {
        // ������������� ���������� ��������� ��� ����� ��������� �� ���������
        currentSpriteIndex = PlayerPrefs.GetInt("DifficultyIndex", 0);
        if (difficultySettings != null)
        {
            CurrentDifficultyData = difficultySettings.GetDifficultyData(currentSpriteIndex);
            Debug.Log($"[DifficultyManager] ���������������� ���������: ������ {currentSpriteIndex}, ����� {CurrentDifficultyData?.time}");
        }
        else
        {
            Debug.LogError("[DifficultyManager] DifficultySettings �� �������� � ����������!");
        }
    }

    void Start()
    {
        if (!CheckComponents()) return; // ���������, ��� �� ���������� ���������

        SetupButtons(); // ����������� ������
        DisplaySprite(); // ���������� ������� ������ ���������
        UpdateButtonState(); // ��������� ��������� ������ (��������/���������)
        LogCurrentSprite(); // �������� ������� ������
        UpdateCurrentDifficultyData(); // ��������� ������ ������� ���������
    }

    private bool CheckComponents()
    {
        bool isValid = true;

        // ��������� ������� ���� �����������
        if (imageComponent == null)
        {
            Debug.LogError("[DifficultyManager] Image Component �� ��������!");
            isValid = false;
        }

        if (sprites == null || sprites.Length == 0)
        {
            Debug.LogError("[DifficultyManager] ������ �������� ����!");
            isValid = false;
        }

        if (nextButton == null)
        {
            Debug.LogError("[DifficultyManager] ������ 'Next' �� ���������");
            isValid = false;
        }

        if (previousButton == null)
        {
            Debug.LogError("[DifficultyManager] ������ 'Previous' �� ���������");
            isValid = false;
        }

        if (confirmButton == null)
        {
            Debug.LogError("[DifficultyManager] ������ 'Confirm' �� ���������");
            isValid = false;
        }

        if (difficultySettings == null)
        {
            Debug.LogError("[DifficultyManager] ���������� ��������� DifficultySettings!");
            isValid = false;
        }

        return isValid;
    }

    private void SetupButtons()
    {
        // ����������� ������ � �������
        nextButton.onClick.AddListener(NextSprite);
        previousButton.onClick.AddListener(PreviousSprite);
        confirmButton.onClick.AddListener(ConfirmDifficulty);
    }

    public void NextSprite()
    {
        if (isChanging) return; // ���� ��� �����������, ����������
        StartCoroutine(ChangeSpriteWithDelay(1)); // ����������� �� ��������� ������
    }

    public void PreviousSprite()
    {
        if (isChanging) return; // ���� ��� �����������, ����������
        StartCoroutine(ChangeSpriteWithDelay(-1)); // ����������� �� ���������� ������
    }

    private IEnumerator ChangeSpriteWithDelay(int direction)
    {
        isChanging = true; // ������������� ����
        int previousSpriteIndex = currentSpriteIndex;

        // �������� ������� ������ ���������
        currentSpriteIndex += direction;
        currentSpriteIndex = Mathf.Clamp(currentSpriteIndex, 0, sprites.Length - 1); // ������������ ������

        yield return new WaitForSeconds(delayBetweenClicks); // �������� ����� ��������������

        DisplaySprite(); // ���������� ����� ������
        UpdateButtonState(); // ��������� ��������� ������
        LogSpriteChange(previousSpriteIndex); // �������� ��������� �������
        UpdateCurrentDifficultyData(); // ��������� ������ ���������
        isChanging = false; // ������� ����
    }

    private void DisplaySprite()
    {
        // ������������� ������� ������ �� Image Component
        if (imageComponent != null && currentSpriteIndex < sprites.Length)
        {
            imageComponent.sprite = sprites[currentSpriteIndex];
        }
    }

    private void UpdateButtonState()
    {
        // ��������/��������� ������ Next � Previous � ����������� �� �������� �������
        previousButton.interactable = (currentSpriteIndex > 0);
        nextButton.interactable = (currentSpriteIndex < sprites.Length - 1);
    }

    private void LogCurrentSprite()
    {
        Debug.Log($"[DifficultyManager] ������������ ������: {sprites[currentSpriteIndex].name} (������: {currentSpriteIndex})");
    }

    private void LogSpriteChange(int previousSpriteIndex)
    {
        if (previousSpriteIndex != currentSpriteIndex)
        {
            Debug.Log($"[DifficultyManager] ������ ������� �: {sprites[previousSpriteIndex].name} (������: {previousSpriteIndex}) " +
                     $"��: {sprites[currentSpriteIndex].name} (������: {currentSpriteIndex})");
        }
        else
        {
            Debug.Log($"[DifficultyManager] ������ ������� �������: {sprites[currentSpriteIndex].name} (������: {currentSpriteIndex})");
        }
    }

    private void UpdateCurrentDifficultyData()
    {
        // ��������� ������� ������ ���������
        if (difficultySettings != null)
        {
            CurrentDifficultyData = difficultySettings.GetDifficultyData(currentSpriteIndex);
            if (CurrentDifficultyData != null)
            {
                Debug.Log($"[DifficultyManager] ��������� ������ ���������: Time = {CurrentDifficultyData.time}, Chance = {CurrentDifficultyData.chance}");
            }
            else
            {
                Debug.LogError("[DifficultyManager] ������ ��������� �� ������� ��� �������� �������!");
            }
        }
    }

    private void ConfirmDifficulty()
    {
        // ��������� ������� ���������
        PlayerPrefs.SetInt("DifficultyIndex", currentSpriteIndex);
        PlayerPrefs.Save();

        Debug.Log($"[DifficultyManager] ������������ ���������: {sprites[currentSpriteIndex].name} " +
                 $"(������: {currentSpriteIndex}), Time = {CurrentDifficultyData.time}, " +
                 $"Chance = {CurrentDifficultyData.chance}");

        // ����� ����� �������� ������ ��� ���������� ������ ������ (��������, �������)
    }

    public DifficultySettings.DifficultyData GetCurrentDifficultyData()
    {
        if (CurrentDifficultyData == null)
        {
            InitializeDefaultDifficulty();
        }
        return CurrentDifficultyData;
    }
}