using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Can hit detection be done this way?
        var other = collision.otherCollider.gameObject;

        if (other is object)
            OnPickup(other);
    }

    private void OnPickup(object player)
    {
        EffectState.instance.Trigger(player);

        // TODO: play animation?

        Destroy(gameObject);
    }
}
