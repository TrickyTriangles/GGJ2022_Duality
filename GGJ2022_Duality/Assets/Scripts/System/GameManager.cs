using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour
{
    public static GameManager instance;

    private bool isPaused;
    public bool IsPaused => isPaused;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        isPaused = false;
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
        }
    }

    public void UnpauseGame()
    {
        isPaused = false;
    }

    public void WinGame()
    {
        Debug.Log("Player has won");
        // Show win screen/effects and go to menu
    }

    public void LoseGame()
    {
        Debug.Log("Player has lost");
        // Show lose screen/effects and go to menu
    }

}
