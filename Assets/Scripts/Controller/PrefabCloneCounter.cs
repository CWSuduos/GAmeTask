using UnityEngine;
using UnityEngine.UI; // ��� ������������ Text

public class ScorePrefabCounter : MonoBehaviour
{
    // ������ �� ��������� ���� ��� ����������� �����
    public Text scoreTextField;

    // ������� ������������ ������
    private int deathCount = 0;

   

    // ����� ��� ���������� �����
    public void IncrementScore()
    {
        deathCount++;
        UpdateScoreText();
    }

    // ����� ��� ���������� ������
    private void UpdateScoreText()
    {
        if (scoreTextField != null)
        {
            scoreTextField.text = $"����: {deathCount}";
        }
    }
}