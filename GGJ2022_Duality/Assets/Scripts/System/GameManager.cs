using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour
{
    public static GameManager instance;

    private bool isPaused;
    public bool IsPaused => isPaused;

    private bool gameHasConcluded;
    public bool hasConcluded => gameHasConcluded;

    #region Delegates

    private Action GameLost;
    public void Subscribe_GameLost(Action sub) { GameLost += sub; }
    public void Unsubscribe_GameLost(Action sub) { GameLost -= sub; }

    #endregion

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
        gameHasConcluded = false;
        isPaused = false;
    }

    public void PauseGame()
    {
        if (!isPaused && !gameHasConcluded)
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
        if (!gameHasConcluded)
        {
            gameHasConcluded = true;
            SceneManager.LoadScene("GameWinScene", LoadSceneMode.Additive);
        }
    }

    public void LoseGame()
    {
        if (!gameHasConcluded)
        {
            GameLost?.Invoke();
            gameHasConcluded = true;
            SceneManager.LoadScene("GameOverScene", LoadSceneMode.Additive);
        }
    }

}
