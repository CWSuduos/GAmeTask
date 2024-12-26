using UnityEngine;
using UnityEngine.UI;

public class DestroyedObjectsCounter : MonoBehaviour
{
    public Text countText; // —сылка на UI Text компонент
    public Text maxScoreText; // —сылка на UI Text компонент дл€ максимального счЄта
    private static int destroyedCount = 0;
    private static int maxScore = 0;
    public Text DaubleCountText;
    void Start()
    {
        if (countText == null)
        {
            Debug.Log("Text component for count display is not assigned in the inspector!");
        }
        if (maxScoreText == null)
        {
            Debug.Log("Text component for max score display is not assigned in the inspector!");
        }
        UpdateCountDisplay();
        UpdateMaxScoreDisplay();
    }

    public static void IncrementDestroyedCount()
    {
        destroyedCount++;
        if (destroyedCount > maxScore)
        {
            maxScore = destroyedCount;
        }
    }

    void UpdateCountDisplay()
    {
        if (countText != null)
        {
            countText.text = "Score: " + destroyedCount;
            DaubleCountText.text = countText.text; 
        }
    }

    void UpdateMaxScoreDisplay()
    {
        if (maxScoreText != null)
        {
            maxScoreText.text = "Max: " + maxScore;
        }
    }

    public void ObjectDestroyed()
    {
        IncrementDestroyedCount();
        UpdateCountDisplay();
        UpdateMaxScoreDisplay();
    }

    public void SetMaxScore(int score)
    {
        maxScore = score;
        UpdateMaxScoreDisplay();
    }
}