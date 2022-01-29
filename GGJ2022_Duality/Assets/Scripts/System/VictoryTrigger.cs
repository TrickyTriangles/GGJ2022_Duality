using System;
using UnityEngine;
using UnityEngine.Events;

public class VictoryTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent PlayerWon;
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!hasTriggered)
            {
                hasTriggered = true;
                PlayerWon?.Invoke();
            }
        }
    }
}
