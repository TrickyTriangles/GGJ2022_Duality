using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialZone : MonoBehaviour
{
    [SerializeField] private TutorialText tutorialTextObject;
    [SerializeField] [Multiline] private string message;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (tutorialTextObject != null)
            {
                tutorialTextObject.EnteredTutorialZone();
                tutorialTextObject.ChangeMessage(message);
            }
        }  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (tutorialTextObject != null)
            {
                tutorialTextObject.ExitedTutorialZone();
            }
        }      
    }
}
