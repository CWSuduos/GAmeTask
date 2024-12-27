using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
    // Метод, вызываемый при уничтожении клона
    private void OnDestroy()
    {
        // Увеличиваем счёт через статический метод ScoreManager
        Score.IncrementScore();
    }
}
