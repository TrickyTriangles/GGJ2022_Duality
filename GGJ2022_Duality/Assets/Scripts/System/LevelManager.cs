using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
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
}
