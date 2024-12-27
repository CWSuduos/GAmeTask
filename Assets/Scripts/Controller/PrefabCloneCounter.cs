using UnityEngine;
using UnityEngine.UI; // Для стандартного Text

public class ScorePrefabCounter : MonoBehaviour
{
    // Ссылка на текстовое поле для отображения счёта
    public Text scoreTextField;

    // Счётчик уничтоженных клонов
    private int deathCount = 0;

   

    // Метод для увеличения счёта
    public void IncrementScore()
    {
        deathCount++;
        UpdateScoreText();
    }

    // Метод для обновления текста
    private void UpdateScoreText()
    {
        if (scoreTextField != null)
        {
            scoreTextField.text = $"Счёт: {deathCount}";
        }
    }
}