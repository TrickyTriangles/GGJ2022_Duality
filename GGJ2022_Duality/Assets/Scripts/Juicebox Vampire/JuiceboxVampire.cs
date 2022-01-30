using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceboxVampire : MonoBehaviour, ICollisionHandler
{
    [SerializeField]
    private AnyStateAnimator animator;

    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        AnyStateAnimation[] animations = new AnyStateAnimation[]
        {
            new AnyStateAnimation("Juicebox_Idle"),
            new AnyStateAnimation("Juicebox_Attack"),
            new AnyStateAnimation("Juicebox_Die"),
        };

        animator.AddAnimations(animations);
    }

    public void CollisionEnter(string colliderName, GameObject other)
    {
        if (isAlive && colliderName == "DamageArea" && other.tag == "Player")
        {
            Debug.Log("Juicebox Vampire triggered with player");
            if (!(other.GetComponent<PlayerController>()?.TakeHit() ?? other.GetComponentInParent<PlayerController>().TakeHit())) {
                Debug.Log("Juicebox dies");
                animator.TryPlayAnimation("Juicebox_Die");
                isAlive = false;
                GetComponent<PacingMovement>().enabled = false;
            } else {
                animator.TryPlayAnimation("Juicebox_Attack");
            }
        }
    }
}
