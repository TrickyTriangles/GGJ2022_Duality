using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float fadeSpeed = 0.05f;
    private Color myColor;
    private float transparency = 0f;

    private int tutorialZoneContacts = 0;
    private bool isContactingTutorialZone => (tutorialZoneContacts > 0);

    private void Start()
    {
        myColor = text.color;
        myColor.a = transparency;

        text.color = myColor;
    }

    private void Update()
    {
        if (isContactingTutorialZone)
        {
            transparency = Mathf.MoveTowards(transparency, 1f, fadeSpeed);
        }
        else
        {
            transparency = Mathf.MoveTowards(transparency, 0f, fadeSpeed);
        }

        myColor.a = transparency;
        text.color = myColor;
    }

    public void ChangeMessage(string newMessage)
    {
        text.text = newMessage;
    }

    public void EnteredTutorialZone()
    {
        tutorialZoneContacts++;
    }

    public void ExitedTutorialZone()
    {
        tutorialZoneContacts--;
    }
}
