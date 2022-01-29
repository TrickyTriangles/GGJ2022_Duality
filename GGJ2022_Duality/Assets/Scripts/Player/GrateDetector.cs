using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrateDetector : MonoBehaviour
{
    private int grates = 0;
    public bool isTouchingGrate => (grates > 0);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        grates++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        grates--;
    }
}
