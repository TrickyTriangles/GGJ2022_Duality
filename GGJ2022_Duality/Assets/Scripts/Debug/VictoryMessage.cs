using System.Collections;
using UnityEngine;
using TMPro;

public class VictoryMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private VictoryTrigger trigger;
    [SerializeField] private float timeToFade = 1f;

    public void GameStateTrigger_PlayerWon()
    {
        StartCoroutine(FadeInTextRoutine());
    }

    private IEnumerator FadeInTextRoutine()
    {
        if (text != null)
        {
            Color newColor = new Color(1f, 1f, 1f, 0f);
            float timer = 0f;

            while (timer < timeToFade)
            {
                timer += Time.deltaTime;
                float ratio = timer / timeToFade;

                newColor.a = ratio;
                text.color = newColor;

                yield return null;
            }
        }
    }
}
