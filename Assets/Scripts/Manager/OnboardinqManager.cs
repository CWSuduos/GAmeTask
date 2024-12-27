using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Onboarding : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image image;
    [SerializeField] private Button nextButton;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float moveDistance = 1000f;

    private int currentIndex = 0;
    private bool isAnimating;
    private RectTransform imageRect;
    private Vector2 startPosition;

    private void Start()
    {
        imageRect = image.GetComponent<RectTransform>();
        startPosition = imageRect.anchoredPosition;

        image.sprite = sprites[0];
        image.color = new Color(1, 1, 1, 0);
        image.DOFade(1f, fadeDuration);
    }

    public void SwipeLeft()
    {
        if (currentIndex > 0 && !isAnimating)
        {
            isAnimating = true;
            imageRect.DOAnchorPosX(startPosition.x + moveDistance, fadeDuration).OnComplete(() =>
            {
                currentIndex--;
                image.sprite = sprites[currentIndex];
                imageRect.anchoredPosition = new Vector2(startPosition.x - moveDistance, startPosition.y);
                imageRect.DOAnchorPosX(startPosition.x, fadeDuration).OnComplete(() =>
                {
                    isAnimating = false;
                });
            });
        }
    }

    public void SwipeRight()
    {
        if (currentIndex < sprites.Length - 1 && !isAnimating)
        {
            isAnimating = true;
            imageRect.DOAnchorPosX(startPosition.x - moveDistance, fadeDuration).OnComplete(() =>
            {
                currentIndex++;
                image.sprite = sprites[currentIndex];
                imageRect.anchoredPosition = new Vector2(startPosition.x + moveDistance, startPosition.y);
                imageRect.DOAnchorPosX(startPosition.x, fadeDuration).OnComplete(() =>
                {
                    isAnimating = false;
                });
            });
        }
    }

    public void NextButtonClicked()
    {
        if (currentIndex < sprites.Length - 1)
        {
            SwipeRight();
        }
        else
        {
            image.DOFade(0f, fadeDuration);
            nextButton.transform.DOScale(0f, fadeDuration).OnComplete(() =>
            {
                SceneManager.LoadScene("MainMenu");
            });
        }
    }
}