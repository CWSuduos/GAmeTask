using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
    // �����, ���������� ��� ����������� �����
    private void OnDestroy()
    {
        // ����������� ���� ����� ����������� ����� ScoreManager
        Score.IncrementScore();
    }
}
