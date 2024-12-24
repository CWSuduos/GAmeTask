using UnityEngine;
using UnityEngine.UI;

public class SpritePrefabMatcher : MonoBehaviour
{
    [Header("UI Elements")]
    public Image displayImage; // ���� ��� �������� �����������

    [Header("Sprites and Prefabs")]
    public Sprite[] sprites; // ������ ��������
    public GameObject[] prefabs; // ������ ��������, ��������������� ��������

    private Sprite currentSprite; // ������� �������� ������
    private bool isSpecial; // ����, �����������, �������� �� ������ ���������

    void Start()
    {
        if (displayImage == null)
        {
            Debug.LogError("���� displayImage �� ���������! ��������� ��������� Image � ����������.");
            return;
        }

        // �������� ������ ��������
        if (sprites.Length != prefabs.Length)
        {
            Debug.LogError("���������� �������� � �������� ������ ���������!");
            return;
        }

        // ������������� �������� �������
        currentSprite = displayImage.sprite;

        if (currentSprite == null)
        {
            Debug.LogWarning("��������� ������ � displayImage ����� null!");
        }

        CheckActiveSprite();
    }

    void Update()
    {
        
        // ���������, ��������� �� ������� ������
        if (currentSprite != displayImage.sprite)
        {
            currentSprite = displayImage.sprite;
            CheckActiveSprite();
        }
    }

    // ����� ��� �������� �������� ��������� �������
    void CheckActiveSprite()
    {
        if (currentSprite == null)
        {
            Debug.LogWarning("currentSprite ����� null! ���������� ��������.");
            return;
        }

        for (int i = 0; i < sprites.Length; i++)
        {
            // ���������, ��� ������� � ������� �� ����� null
            if (sprites[i] == null)
            {
                Debug.LogWarning($"sprites[{i}] ����� null! ���������� ���� �������.");
                continue;
            }

            if (prefabs[i] == null)
            {
                Debug.LogWarning($"prefabs[{i}] ����� null! ���������� ���� �������.");
                continue;
            }

            // ���������� ������� ������
            if (sprites[i] == currentSprite)
            {
                Debug.Log($"������� �����������: {currentSprite.name}, ��������� ������: {prefabs[i].name}");

                // ����������, �������� �� ������ ���������
                isSpecial = IsSpriteSpecial(currentSprite);

                // ��������� ��������� �������� � �������
                ApplySpecialProperty(prefabs[i], isSpecial);

                return;
            }
        }

        Debug.LogWarning("������� ������ �� ������ � ������� ��������!");
    }

    // ����� ��� �����������, �������� �� ������ ���������
    bool IsSpriteSpecial(Sprite sprite)
    {
        if (sprite == null)
        {
            Debug.LogWarning("Sprite ����� null � ������ IsSpriteSpecial!");
            return false;
        }

        // �������� ����� �������
        return sprite.name.Contains("Special");
    }

    // ����� ��� ���������� "���������� ��������" �������
    void ApplySpecialProperty(GameObject prefab, bool isSpecial)
    {
        if (prefab == null)
        {
            Debug.LogWarning("Prefab ����� null � ������ ApplySpecialProperty!");
            return;
        }

        Debug.Log($"��������� ��������� �������� ��� �������: {prefab.name}, ���������: {isSpecial}");

        // ������ ��� ���������� �����
        if (isSpecial)
        {
            Debug.Log("���� ���������!");
            // ����� ����� ������� ����� ��� ���������� �����, ��������:
            // ScoreManager.Instance.AddPoints(10);
        }
        else
        {
            Debug.Log("���� �� ���������.");
        }

        // �������������� ������ (���� ���������)
    }
}