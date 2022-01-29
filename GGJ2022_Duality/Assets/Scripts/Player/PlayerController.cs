using System;
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
    [SerializeField] private CircleCollider2D mainCollider;
    [SerializeField] private CircleCollider2D spikeCollider;

    [Header("Game Values")]
    [SerializeField] private float moveForce = 100f;
    [SerializeField] private float jumpForce = 500f;
    private PlayerState state = PlayerState.JELLY;
    private Vector3 lastHitPosition;

    [Header("Physics")]
    [SerializeField] private PhysicsMaterial2D jellyMaterial;
    [SerializeField] private PhysicsMaterial2D spikeMaterial;
    [Tooltip("What layers the raycast performed while jumping will detect.")]
    [SerializeField] private LayerMask jumpLayers;

    #region Delegates and Subscriber Methods

    private Action<Vector3> CollidedWithSurface;
    public void Subscribe_CollidedWithSurface(Action<Vector3> sub) { CollidedWithSurface += sub; }
    public void Unsubscribe_CollidedWithSurface(Action<Vector3> sub) { CollidedWithSurface -= sub; }

    #endregion

    private void Start()
    {
        Subscribe_CollidedWithSurface(UpdateLastHitPosition);
        jellyForm?.SetActive(true);
        spikeForm?.SetActive(false);
    }

    private void UpdateLastHitPosition(Vector3 hitPosition)
    {
        lastHitPosition = hitPosition;
    }

    private void Update()
    {
        Vector2 inputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        myRigidbody?.AddForce(inputs * moveForce * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleJump(); 
        }

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

    private void HandleJump()
    {
        if (spikeCollider != null)
        {
            Vector2 direction = lastHitPosition - transform.position;
            float radius = (spikeCollider.radius * spikeCollider.gameObject.transform.localScale.x) + 0.05f;

            if (Physics2D.Raycast(transform.position, direction, radius, jumpLayers))
            {
                myRigidbody?.AddForce(direction.normalized * jumpForce);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollidedWithSurface?.Invoke(collision.GetContact(0).point);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CollidedWithSurface?.Invoke(collision.GetContact(0).point);
    }

    private void OnDestroy()
    {
        Unsubscribe_CollidedWithSurface(UpdateLastHitPosition);
    }
}
