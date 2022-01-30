using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AnimationTriggerEvent(string animation);

public class AnyStateAnimator : MonoBehaviour
{
    private Animator animator;

    private Dictionary<string, AnyStateAnimation> animations = new Dictionary<string, AnyStateAnimation>();

    public AnimationTriggerEvent AnimationTriggerEvent { get; set; }

    private string currentAnimation = string.Empty;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Animate();
    }

    public void AddAnimations(params AnyStateAnimation[] newAnimations)
    {
        for (int i = 0; i < newAnimations.Length; i++)
        {
            this.animations.Add(newAnimations[i].Name, newAnimations[i]);
        }
    }

    public void TryPlayAnimation(string newAnimation)
    {
        if (currentAnimation == "")
        {
            animations[newAnimation].Active = true;
            currentAnimation = newAnimation;
        }
        else if (currentAnimation != newAnimation && !((IList)animations[newAnimation].HigherPrio).Contains(currentAnimation) || !animations[currentAnimation].Active)
        {
            animations[currentAnimation].Active = false;
            animations[newAnimation].Active = true;
            currentAnimation = newAnimation;
        }
    }

    private void Animate()
    {
        foreach (string key in animations.Keys)
        {
            animator.SetBool(key, animations[key].Active);
        }
    }

    public void OnAnimationDone(string animation)
    {
        animations[animation].Active = false;
    }

    public void OnAnimationTrigger(string animation)
    {
        if (AnimationTriggerEvent != null)
        {
            AnimationTriggerEvent.Invoke(animation);
        }
    }
}