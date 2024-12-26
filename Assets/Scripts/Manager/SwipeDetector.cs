using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    public Onboarding onboarding; // ������ �� ������ Onboarding

    private void Update()
    {
        // ��������� ���������
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 swipeStartPos = touch.position;
                if (touch.phase == TouchPhase.Ended)
                {
                    Vector2 swipeEndPos = touch.position;
                    Vector2 swipeDelta = swipeEndPos - swipeStartPos;
                    if (swipeDelta.magnitude > 10f)
                    {
                        // ��������� ���������
                        if (swipeDelta.x > 0)
                        {
                            // ��������� ������
                            onboarding.SwipeRight();
                        }
                        else if (swipeDelta.x < 0)
                        {
                            // ��������� �����
                            onboarding.SwipeLeft();
                        }
                    }
                }
            }
        }
    }
}