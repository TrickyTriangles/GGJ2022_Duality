using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverSceneManager : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Animator gameOverTextAnimator;

    private float fadeInTime = 2f;
    private float fadeInTargetAlpha = 0.5f;
    private float fadeInTargetTimescale = 0.2f;
    private float fadeOutTime = 1f;

    private void Start()
    {
        gameOverTextAnimator.StopPlayback();
        StartCoroutine(FadeInRoutine());
    }

    private IEnumerator FadeInRoutine()
    {
        Color currentColor = background.color;
        currentColor.a = 0f;

        float timer = 0f;
        bool animationStarted = false;

        while (timer < fadeInTime)
        {
            timer += Time.unscaledDeltaTime;
            float ratio = timer / fadeInTime;

            if (ratio >= 0.5f && !animationStarted)
            {
                gameOverTextAnimator.Play("GameOverText");
            }

            currentColor.a = fadeInTargetAlpha * ratio;
            background.color = currentColor;

            Time.timeScale = Mathf.Lerp(1f, fadeInTargetTimescale, ratio);

            yield return null;
        }

        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        Color backgroundColor = background.color;
        float backgroundStartingAlpha = backgroundColor.a;
        Color textColor = gameOverText.color;
        float timer = 0f;

        while (timer < fadeOutTime)
        {
            timer += Time.unscaledDeltaTime;
            float ratio = timer / fadeOutTime;

            backgroundColor.a = Mathf.Lerp(backgroundStartingAlpha, 1f, ratio);
            background.color = backgroundColor;

            textColor.a = Mathf.Lerp(1f, 0f, ratio);
            gameOverText.color = textColor;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }    
}
