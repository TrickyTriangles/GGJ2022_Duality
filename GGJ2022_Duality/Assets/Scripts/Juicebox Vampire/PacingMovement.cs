using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacingMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3[] positions;

    private int index = 0;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, positions[index], Time.deltaTime * speed);

        if (transform.position == positions[index]) {
            if (index == positions.Length - 1) {
                index = 0;
            } else {
                index++;
            }
        }
    }
}
