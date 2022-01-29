using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private float zOffset = 10f;

    private void Update()
    {
        if (target != null)
        {
            transform.position = target.transform.position - new Vector3(0f, 0f, zOffset);
        }
    }
}
