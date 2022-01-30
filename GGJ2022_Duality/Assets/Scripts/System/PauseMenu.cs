using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI header;
    [SerializeField] private TextMeshProUGUI continueOption;
    [SerializeField] private TextMeshProUGUI menuOption;
    private int selection = 0;

    [Header("Menu Colors")]
    [SerializeField] private Color color1;
    [SerializeField] private Color color2;

    [Header("Scene Transition Values")]
    [SerializeField] private float fadeoutTime = 2f;
    [SerializeField] private string goToScene = "MainMenu";

    private bool hasMoved = false;
    private bool hasChosen = false;

    private void Start()
    {
        Time.timeScale = 0f;
        StartCoroutine(FadeHeaderRoutine());
        StartCoroutine(TextColorCycleRoutine());
    }

    private void Update()
    {
        if (!hasChosen)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                if (selection == 0)
                {
                    // Continue
                    Time.timeScale = 1f;
                    SceneManager.UnloadSceneAsync("PauseScene");
                }
                else if (selection == 1)
                {
                    // Main menu
                    StartCoroutine(FadeToMenuRoutine());
                }

                hasChosen = true;
            }

            if (Input.GetAxisRaw("Vertical") > 0f && !hasMoved)
            {
                hasMoved = true;
                selection += 1;
                selection %= 2;
            }
            else if (Input.GetAxisRaw("Vertical") < 0f && !hasMoved)
            {
                hasMoved = true;
                selection -= 1;
                selection %= 2;
            }

            if (Input.GetAxisRaw("Vertical") == 0f)
            {
                hasMoved = false;
            }
        }
    }

    private IEnumerator FadeHeaderRoutine()
    {
        Color newColor = new Color(1f, 1f, 1f, 1f);
        float timer = 0f;

        while (true)
        {
            timer += Time.unscaledDeltaTime;
            newColor.a = 0.5f + Mathf.Sin(timer) * 0.2f;

            header.color = newColor;

            yield return null;
        }
    }

    private IEnumerator TextColorCycleRoutine()
    {
        float timer = 0f;

        while (true)
        {
            timer += Time.unscaledDeltaTime;
            float lerp = 0.5f + Mathf.Sin(timer * 3f) * 0.5f;

            if (selection == 0)
            {
                continueOption.color = Color.Lerp(color1, color2, lerp);
                menuOption.color = Color.white;
            }
            else
            {
                continueOption.color = Color.white;
                menuOption.color = Color.Lerp(color1, color2, lerp);
            }

            yield return null;
        }
    }

    private IEnumerator FadeToMenuRoutine()
    {
        float timer = 0f;
        Color backgroundColor = background.color;

        header.gameObject.SetActive(false);
        continueOption.gameObject.SetActive(false);
        menuOption.gameObject.SetActive(false);

        while (timer < fadeoutTime)
        {
            timer += Time.unscaledDeltaTime;
            float ratio = timer / fadeoutTime;

            background.color = Color.Lerp(backgroundColor, Color.black, ratio);
            yield return null;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(goToScene);
    }
}
