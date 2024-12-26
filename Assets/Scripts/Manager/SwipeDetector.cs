using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    public Onboarding onboarding; // Ссылка на скрипт Onboarding

    private void Update()
    {
        // Проверить свайпание
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
                        // Свайпание произошло
                        if (swipeDelta.x > 0)
                        {
                            // Свайпание вправо
                            onboarding.SwipeRight();
                        }
                        else if (swipeDelta.x < 0)
                        {
                            // Свайпание влево
                            onboarding.SwipeLeft();
                        }
                    }
                }
            }
        }
    }
}