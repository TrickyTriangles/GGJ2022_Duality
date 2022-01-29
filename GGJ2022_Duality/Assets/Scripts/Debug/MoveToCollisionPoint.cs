using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCollisionPoint : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void Awake()
    {
        if (player != null)
        {
            player.Subscribe_CollidedWithSurface(PlayerController_CollidedWithSurface);
        }
    }

    private void PlayerController_CollidedWithSurface(Vector3 collisionPoint)
    {
        transform.position = collisionPoint;
    }

    private void OnDestroy()
    {
        if (player != null)
        {
            player.Unsubscribe_CollidedWithSurface(PlayerController_CollidedWithSurface);
        }
    }
}
