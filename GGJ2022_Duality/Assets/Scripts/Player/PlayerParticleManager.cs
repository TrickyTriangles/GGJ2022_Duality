using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject jellyParticle;
    [SerializeField] private float velocityThreshold = 5f;

    [SerializeField] private GameObject jellyParticleSmall;
    [SerializeField] private float jellySmallVelocityThreshold = 2f;

    private void Start()
    {
        if (player != null)
        {
            player.Subscribe_ActivateJellyParticle(PlayerController_ActivateJellyParticle);
        }
    }

    private void PlayerController_ActivateJellyParticle(Collision2D collision, float velocity)
    {
        if (velocity >= velocityThreshold)
        {
            if (jellyParticle != null)
            {
                GameObject newParticle = Instantiate(jellyParticle, transform.position, Quaternion.identity);
                newParticle.transform.up = collision.GetContact(0).normal;
                return;
            }          
        }

        if (velocity >= jellySmallVelocityThreshold)
        {
            if (jellyParticleSmall != null)
            {
                GameObject newParticle = Instantiate(jellyParticleSmall, transform.position, Quaternion.identity);
                newParticle.transform.up = collision.GetContact(0).normal;
                return;
            }
        }
    }

    private void OnDestroy()
    {
        if (player != null)
        {
            player.Unsubscribe_ActivateJellyParticle(PlayerController_ActivateJellyParticle);
        }
    }
}
