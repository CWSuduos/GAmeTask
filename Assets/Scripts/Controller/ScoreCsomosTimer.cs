using UnityEngine;
using UnityEngine.UI;

public class ScoreCsomosTimer : MonoBehaviour
{
    public Text scoreText;      // ��������� ���� ��� �������� �����
    public Text maxScoreText;   // ��������� ���� ��� ������������� �����
    public Button startButton;  // ������ ��� ������� ��������

    private int maxScore = 0;   // ������������ ����
    private int score = 0;      // ������� ����
    private bool isCounting = false;    // ����, ���������, ������� �� �������
    private bool isPlayerAlive = true;  // ���� ��������� ������

    private float timer = 0f;   // ������ ��� ������� ������

    void Start()
    {
        // ��������� �������� ��� ������
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartCounting);
        }

        // ������������� �� ������� ������ ������
        PlayerHealthCosmos.OnPlayerDeath.AddListener(OnPlayerDeath);

        UpdateScoreText();
        UpdateMaxScoreText();
    }

    void Update()
    {
        if (isCounting && isPlayerAlive)
        {
            timer += Time.deltaTime;

            // ������ ��������� �������
            if (timer >= 1f)
            {
                score += 1;     // ����������� ����
                timer = 0f;     // ���������� ������

                UpdateScoreText();

                // ��������� � ��������� ������������ ����
                if (score > maxScore)
                {
                    maxScore = score;
                    UpdateMaxScoreText();
                }
            }
        }
    }

    public void StartCounting()
    {
        if (isPlayerAlive) // ��������� ������� ������ ���� ����� ���
        {
            isCounting = true;
        }
    }

    public void StopCounting()
    {
        isCounting = false; // ������������� �������
    }

    // �����, ���������� ��� ������ ������
    private void OnPlayerDeath()
    {
        isPlayerAlive = false; // ����� ����
        StopCounting();        // ������������� ������� �����
        Debug.Log("������� ����� ���������� ��-�� ������ ������.");
    }

    // ��������� ����� �������� �����
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }

    // ��������� ����� ������������� �����
    void UpdateMaxScoreText()
    {
        if (maxScoreText != null)
        {
            maxScoreText.text = $"{maxScore}";
        }
    }
}