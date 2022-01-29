using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum PlayerState { JELLY, SPIKE }

    [Header("Player Components")]
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private GameObject jellyForm;
    [SerializeField] private GameObject spikeForm;

    [Header("Game Values")]
    [SerializeField] private float moveForce = 100f;
    [SerializeField] private float jumpForce = 500f;
    private PlayerState state = PlayerState.JELLY;

    [Header("Physics")]
    [SerializeField] private PhysicsMaterial2D jellyMaterial;
    [SerializeField] private PhysicsMaterial2D spikeMaterial;

    private void Start()
    {
        jellyForm?.SetActive(true);
        spikeForm?.SetActive(false);
    }

    private void Update()
    {
        Vector2 inputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        myRigidbody?.AddForce(inputs * moveForce * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space))
        {
            state = PlayerState.SPIKE;
            jellyForm.SetActive(false);
            spikeForm.SetActive(true);
            myRigidbody.sharedMaterial = spikeMaterial;
        }
        else
        {
            state = PlayerState.JELLY;
            jellyForm.SetActive(true);
            spikeForm.SetActive(false);
            myRigidbody.sharedMaterial = jellyMaterial;
        }
    }
}
