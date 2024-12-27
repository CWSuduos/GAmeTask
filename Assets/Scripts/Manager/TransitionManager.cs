using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private CanvasGroup panelCanvasGroup;
    [SerializeField] private float loadingDuration = 1f;

    public void LoadSceneWithAnimation(string sceneName, GameObject loadingImage, GameObject sliderObject, GameObject buttonObject)
    {
        if (buttonObject != null) buttonObject.SetActive(true);
        if (sliderObject != null) sliderObject.SetActive(true);
        if (loadingImage != null) loadingImage.SetActive(true);

        StartLoadingAnimation(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }

    private void StartLoadingAnimation(System.Action onComplete)
    {
        gameObject.SetActive(true);

        Vector3 startScale = new Vector3(0.1f, 0.1f, 0.1f);
        Vector3 endScale = Vector3.one;

        loadingSlider.value = 0f;
        panelCanvasGroup.alpha = 0f;
        transform.localScale = startScale;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(endScale, 0.5f).SetEase(Ease.OutBack))
            .Join(panelCanvasGroup.DOFade(1f, 0.5f))
            .AppendCallback(() =>
            {
                DOTween.To(() => loadingSlider.value, x => loadingSlider.value = x, 1f, loadingDuration)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        onComplete?.Invoke();
                    });
            });

    }
}