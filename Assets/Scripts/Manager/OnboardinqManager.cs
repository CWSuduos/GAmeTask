using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Onboarding : MonoBehaviour
{
    public Sprite[] sprites; 
    public Image image; 
    public Button nextButton; 
    public int currentIndex = 0;

    private void Start()
    {
        // Показать первый спрайт
        image.sprite = sprites[0];
    }

    public void SwipeLeft()
    {
        
        if (currentIndex > 0)
        {
            currentIndex--;
            image.sprite = sprites[currentIndex];
        }
    }

    public void SwipeRight()
    {
       
        if (currentIndex < sprites.Length - 1)
        {
            currentIndex++;
            image.sprite = sprites[currentIndex];
        }
    }

    public void NextButtonClicked()
    {
        
        if (currentIndex < sprites.Length - 1)
        {
            currentIndex++;
            image.sprite = sprites[currentIndex];
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

