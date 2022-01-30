using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMover : MonoBehaviour
{
    private enum SineMode { SINE, COSINE }

    [SerializeField] private SineMode mode = SineMode.SINE;
    [SerializeField] private float period = 1f;
    [SerializeField] private float magnitude = 1f;

    [Header("Axis")]
    [SerializeField] private bool x;
    public float X => x ? 1 : 0;

    [SerializeField] private bool y;
    public float Y => y ? 1 : 0;

    [SerializeField] private bool z;
    public float Z => z ? 1 : 0;

    private Vector3 startPos;
    private float timer = 0f;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        switch (mode)
        {
            case SineMode.SINE:
                transform.position = startPos + new Vector3(
                    X * Mathf.Sin(timer * period) * magnitude,
                    Y * Mathf.Sin(timer * period) * magnitude,
                    Z * Mathf.Sin(timer * period) * magnitude);
                break;
            case SineMode.COSINE:
                transform.position = startPos + new Vector3(
                    X * Mathf.Cos(timer * period) * magnitude,
                    Y * Mathf.Cos(timer * period) * magnitude,
                    Z * Mathf.Cos(timer * period) * magnitude);
                break;
            default:
                break;
        }
    }
}
