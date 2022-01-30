using System;
using UnityEngine;
using UnityEngine.Events;

public enum STATES { WIN, LOSE };
public class GameStateTrigger : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private STATES triggeredState;
    [SerializeField] private UnityEvent PlayerWon;
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!hasTriggered)
            {
                hasTriggered = true;
                switch (triggeredState) {
                    case STATES.WIN:
                        PlayerWon?.Invoke();
                        levelManager.Win();
                        break;
                    case STATES.LOSE:
                        levelManager.Lose();
                        break;
                    default:
                        Debug.Log("No state selected");
                        break;
                }
            }
        }
    }
}
