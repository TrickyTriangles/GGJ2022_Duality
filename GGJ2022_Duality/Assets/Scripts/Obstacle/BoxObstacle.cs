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
        rb.mass = 0.1f;
        rb.AddForce(new Vector2(Random.Range(-100f, 100f), Random.Range(-100f, 100f)));
        gameObject.layer = LayerMask.NameToLayer("Particle");
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        Color currentColor = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
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
