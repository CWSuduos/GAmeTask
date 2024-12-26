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

    private DifficultyBasedTimer difficultyTimer; // ������ �� DifficultyBasedTimer

    void Start()
    {
        // ��������� ������������ ���� �� PlayerPrefs
        maxScore = PlayerPrefs.GetInt("MaxScoreCosmos", 0);

        // ������� DifficultyBasedTimer ��� ������
        difficultyTimer = FindObjectOfType<DifficultyBasedTimer>();

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
            // ���������, �� ���������� �� ������
            if (difficultyTimer != null && !difficultyTimer.IsTimerRunning)
            {
                StopCounting();
                return; // ������� �� Update, ����� �������� ����������� ���������� �����
            }

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
                    PlayerPrefs.SetInt("MaxScoreCosmos", maxScore); // ��������� ������������ ���� � PlayerPrefs
                    PlayerPrefs.Save(); // ���� ��������� ������
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
        isCounting = false;
        Debug.Log("������� ����� ����������.");
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
            maxScoreText.text = $"Max: {maxScore}";
        }
    }
}