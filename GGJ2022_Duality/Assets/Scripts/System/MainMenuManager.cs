using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string gameScene = "GreyboxLevel";
    [SerializeField] private ParticleSystem starfruitParticle;
    [SerializeField] private ParticleSystem jellyParticle;
    private bool hasChosen = false;

    [Header("UI Components")]
    [SerializeField] private Image background;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void QuitGame()
    {
        if (!hasChosen)
        {
            Application.Quit();
        }
    }

    public void StartGame()
    {
        if (hasChosen) return;

        hasChosen = true;
        StartCoroutine(StartGameRoutine());
    }

    private IEnumerator StartGameRoutine()
    {
        if (starfruitParticle != null) starfruitParticle.Stop();
        if (jellyParticle != null) jellyParticle.Play();

        Color backgroundColor = background.color;
        float timer = 0f;

        yield return new WaitForSeconds(2f);

        while (timer < 2f)
        {
            timer += Time.deltaTime;
            float ratio = timer / 2f;

            background.color = Color.Lerp(backgroundColor, Color.black, ratio);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(gameScene);
    }
}
