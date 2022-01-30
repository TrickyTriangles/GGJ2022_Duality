using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinSceneManager : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Transform particleTransform;
    private float fadeoutTime = 2f;
    private Camera cameraRef;
    private Vector3 initialOffset;

    private void Start()
    {
        cameraRef = Camera.main;
        initialOffset = particleTransform.position;

        StartCoroutine(SceneRoutine());
    }

    private void Update()
    {
        particleTransform.position = cameraRef.transform.position + initialOffset + new Vector3(0f, 0f, 5f);
    }

    private IEnumerator SceneRoutine()
    {
        Color currentColor = background.color;
        float timer = 0f;

        yield return new WaitForSecondsRealtime(5f);

        while (timer < fadeoutTime)
        {
            timer += Time.unscaledDeltaTime;
            float ratio = timer / fadeoutTime;

            currentColor.a = Mathf.Lerp(0f, 1f, ratio);
            background.color = currentColor;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        AudioManager.StopMusic();
        SceneManager.LoadScene(0);
    }
}
