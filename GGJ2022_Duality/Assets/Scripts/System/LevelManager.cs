using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.StartGame();
    }

    private void Update()
    {
        if (GameManager.instance != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!GameManager.instance.IsPaused)
                {
                    GameManager.instance.PauseGame();
                }
            }
        }
    }

    public void Win()
    {
        GameManager.instance.LoseGame();
    }

    public void Lose()
    {
        GameManager.instance.WinGame();
    }
}
