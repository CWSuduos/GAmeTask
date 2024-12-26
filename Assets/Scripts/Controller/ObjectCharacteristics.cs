using UnityEngine;

public class ObjectCharacteristics : MonoBehaviour
{
    [Header("Prefabs and Sprites")]
    public GameObject normalDeathEffectPrefab;    // ������� ������ ������
    public GameObject specialDeathEffectPrefab;   // ����������� ������ ������

    [Header("Effect Settings")]
    public float effectDuration = 0.4f;
    public float effectUpwardForce = 5f;

    [Header("Player Death Settings")]
    public float playerDeathAnimationDuration = 0.5f;
    public float playerDeathUpwardForce = 2f;
    public float playerScaleDownSpeed = 2f;

    private bool isPlayerDying = false;
    private SpecialObject specialObject;

    private void Start()
    {
        specialObject = GetComponent<SpecialObject>();
        if (specialObject == null)
        {
            specialObject = gameObject.AddComponent<SpecialObject>();
        }

        if (normalDeathEffectPrefab == null)
        {
            Debug.LogWarning("Normal Death Effect Prefab �� ��������!");
        }
        if (specialDeathEffectPrefab == null)
        {
            Debug.LogWarning("Special Death Effect Prefab �� ��������!");
        }
    }

    private void HandleCollision(Collider2D collision)
    {
        // ���������, ��� ��� ����� � ��� ������ ��� �� ��������
        if (collision == null || !collision.CompareTag("Player") || isPlayerDying)
            return;

        Debug.Log($"[ObjectCharacteristics] ������������ � ��������: {gameObject.name}");

        // ��������� ������� ���������� SpecialObject
        SpecialObject specialObj = GetComponent<SpecialObject>();
        if (specialObj == null)
        {
            Debug.LogWarning($"[ObjectCharacteristics] �� ������� {gameObject.name} ����������� ��������� SpecialObject!");
            return;
        }

        // ����������, ����� ������ ������������
        GameObject effectToUse = normalDeathEffectPrefab;

        // ���������, �������� �� ������ ���������
        if (specialObj.isSpecial)
        {
            Debug.Log($"[ObjectCharacteristics] ������ {gameObject.name} �������� ���������!");

            // ���������� ����������� ������ ���� ������ ���������
            if (specialDeathEffectPrefab != null)
            {
                effectToUse = specialDeathEffectPrefab;
            }

            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddPoints(1, gameObject.name); // ��������� 1 ���� �� ������
                Debug.Log($"[ObjectCharacteristics] ��������� ���� �� ������ ������: {gameObject.name}");
            }
            else
            {
                Debug.LogError("[ObjectCharacteristics] ScoreManager �� ������ �� �����!");
            }
        }
        else
        {
            Debug.Log($"[ObjectCharacteristics] ������ {gameObject.name} �� �������� ���������");
            // �������� ��������, ���� ������ �� ���������
            if (HealthManager.Instance != null)
            {
                Debug.Log("[ObjectCharacteristics] HealthManager ������, �������� TakeDamage");
                HealthManager.Instance.TakeDamage();
                HealthManager.Instance.DebugStatus();
            }
            else
            {
                Debug.Log("[ObjectCharacteristics] HealthManager �� ������ �� �����!");
            }
        }

        // ������ ������ ������
        if (effectToUse != null)
        {
            CreateDeathEffect(effectToUse);
        }
        else
        {
            Debug.Log($"[ObjectCharacteristics] ������ ������ �� �������� ��� ������� {gameObject.name}!");
        }

        // ��������� �������� ������ � ���������� ������
        StartCoroutine(PlayerDeathAnimation(collision.gameObject));
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.collider != null)
        {
            HandleCollision(collision.collider);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            HandleCollision(collision);
        }
    }
    
   

    private void CreateDeathEffect(GameObject effectPrefab)
    {
        GameObject deathEffect = Instantiate(effectPrefab, transform.position, transform.rotation);

        Rigidbody2D effectRb = deathEffect.GetComponent<Rigidbody2D>();
        if (effectRb == null)
        {
            effectRb = deathEffect.AddComponent<Rigidbody2D>();
            effectRb.gravityScale = 0;
        }

        effectRb.velocity = Vector2.up * effectUpwardForce;
        Destroy(deathEffect, effectDuration);
    }

    private System.Collections.IEnumerator PlayerDeathAnimation(GameObject player)
    {
        if (player == null) yield break;

        isPlayerDying = true;

        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb == null)
        {
            playerRb = player.AddComponent<Rigidbody2D>();
            playerRb.gravityScale = 0;
        }

        playerRb.velocity = Vector2.up * playerDeathUpwardForce;

        float timer = 0f;
        Vector3 originalScale = player.transform.localScale;

        while (timer < playerDeathAnimationDuration && player != null)
        {
            timer += Time.deltaTime;
            float scaleFactor = Mathf.Lerp(1f, 0f, timer / playerDeathAnimationDuration * playerScaleDownSpeed);
            player.transform.localScale = originalScale * scaleFactor;
            yield return null;
        }

        isPlayerDying = false;
        if (player != null)
        {
            Destroy(player);
        }
    }
}

public class TimerAffectedObject : MonoBehaviour
{
    private bool isAffectedByTimer = false;

    public void SetAffectedByTimer(bool affected)
    {
        isAffectedByTimer = affected;
    }

    public bool IsAffectedByTimer()
    {
        return isAffectedByTimer;
    }
}