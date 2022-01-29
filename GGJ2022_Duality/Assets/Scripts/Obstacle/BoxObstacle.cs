using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxObstacle : MonoBehaviour, ISpikeHittable
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float fadeoutTime = 3f;

    void ISpikeHittable.Hit()
    {
        gameObject.layer = LayerMask.NameToLayer("Particle");
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        Color currentColor = new Color(1f, 1f, 1f, 1f);
        float timer = 0f;

        while (timer < fadeoutTime)
        {
            timer += Time.deltaTime;
            float alphaRatio = (fadeoutTime - timer) / fadeoutTime;

            currentColor.a = alphaRatio;
            sprite.color = currentColor;

            yield return null;
        }

        Destroy(gameObject);
    }
}
