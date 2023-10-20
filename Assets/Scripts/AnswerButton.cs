using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

[RequireComponent(typeof(Button)), RequireComponent(typeof(CanvasGroup))]
public class AnswerButton : MonoBehaviour
{
    public Button answerButton;
    public TextMeshProUGUI answerText;
    private CanvasGroup canvasGroup;
    private Image buttonImage;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        buttonImage = GetComponent<Image>();
    }

    public void TrueEffect(Action<bool> onEnd)
    {
        FadeEffect(() =>
        {
            buttonImage.color = Color.green;
            answerText.color = Color.green;
            onEnd(true);
        });
    }

    public void NotTrueEffect(Action<bool> onEnd)
    {
        FadeEffect(() =>
        {
            buttonImage.color = Color.red;
            answerText.color = Color.red;
            onEnd(false);
        });
    }

    public void ResetAnswerButton()
    {
        buttonImage.color = Color.white;
        answerText.color = Color.white;
    }

    private void FadeEffect(Action onEnd)
    {
        int lengthOfFadeLoop = 0;

        void FadeLoop()
        {
            if (lengthOfFadeLoop > 1)
            {
                onEnd();
                return;
            };

            canvasGroup.DOFade(0, .55f).onComplete = () =>
            {
                canvasGroup.DOFade(1, .55f).onComplete = () =>
                {
                    lengthOfFadeLoop++;
                    FadeLoop();
                };
            };
        }

        FadeLoop();
    }
}