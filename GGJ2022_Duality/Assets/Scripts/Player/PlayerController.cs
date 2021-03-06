using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum PlayerState { JELLY, SPIKE }

    [Header("Game State")]
    [SerializeField] private LevelManager levelManager;

    [Header("Player Components")]
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private GameObject jellyForm;
    [SerializeField] private GameObject spikeForm;
    [SerializeField] private CircleCollider2D mainCollider;
    [SerializeField] private CircleCollider2D spikeCollider;
    [SerializeField] private GrateDetector grateDetector;
    [SerializeField] private AnyStateAnimator jellyAnimator;

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

    private bool hasDied = false;

    #region Delegates and Subscriber Methods

    private Action<Vector3> CollidedWithSurface;
    public void Subscribe_CollidedWithSurface(Action<Vector3> sub) { CollidedWithSurface += sub; }
    public void Unsubscribe_CollidedWithSurface(Action<Vector3> sub) { CollidedWithSurface -= sub; }

    private Action<Collision2D, float> ActivateJellyParticle;
    public void Subscribe_ActivateJellyParticle(Action<Collision2D, float> sub) { ActivateJellyParticle += sub; }
    public void Unsubscribe_ActivateJellyParticle(Action<Collision2D, float> sub) { ActivateJellyParticle -= sub; }

    private Action OnDeath;
    public void Subscribe_OnDeath(Action sub) { OnDeath += sub; }
    public void Unsubscribe_OnDeath(Action sub) { OnDeath -= sub; }

    #endregion

    private void Start()
    {
        GameManager.instance.Subscribe_GameLost(Kill);
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
        if (!GameManager.instance.IsPaused)
        {
            Vector2 inputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            myRigidbody.AddForce(inputs.normalized * moveForce * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                HandleJump();
            }

            if (!grateDetector.isTouchingGrate)
            {
                HandleFormChange();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                Kill();
            }
        }
    }

    private void HandleFormChange()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            state = PlayerState.SPIKE;
            jellyForm.SetActive(false);
            spikeForm.SetActive(true);
            myRigidbody.sharedMaterial = spikeMaterial;
            gameObject.layer = LayerMask.NameToLayer("Spike");
        }
        else
        {
            state = PlayerState.JELLY;
            jellyForm.SetActive(true);
            spikeForm.SetActive(false);
            myRigidbody.sharedMaterial = jellyMaterial;
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    private void HandleJump()
    {
        if (spikeCollider != null)
        {
            Vector2 direction = lastHitPosition - transform.position;
            float radius = (spikeCollider.radius * spikeCollider.gameObject.transform.localScale.x) + 0.25f;

            if (Physics2D.Raycast(transform.position, direction, radius, jumpLayers))
            {
                myRigidbody.AddForce(-direction.normalized * jumpForce);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollidedWithSurface?.Invoke(collision.GetContact(0).point);
        ActivateJellyParticle?.Invoke(collision, myRigidbody.velocity.magnitude);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CollidedWithSurface?.Invoke(collision.GetContact(0).point);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state == PlayerState.SPIKE)
        {
            ISpikeHittable spikeHittable = collision.gameObject.GetComponent<ISpikeHittable>();

            if (spikeHittable != null)
            {
                spikeHittable.Hit();
            }
        }
    }

    public bool TakeHit()
    {
        if (state == PlayerState.JELLY) {
            Kill();
            return true;
        } else {
            // do damage
            return false;
        }
    }

    public void Kill()
    {
        if (!hasDied)
        {
            hasDied = true;
            OnDeath?.Invoke();
            levelManager.Lose();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.Unsubscribe_GameLost(Kill);
        Unsubscribe_CollidedWithSurface(UpdateLastHitPosition);
    }
}
