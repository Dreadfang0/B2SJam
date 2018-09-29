using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        BasePlayerController player = col.GetComponent<BasePlayerController>();
        if (player != null)
        {
            OnPickup(player);
        }
    }
    private void OnPickup(BasePlayerController player)
    {
        EffectState.instance.Trigger(player);

        // TODO: play animation?

        Destroy(gameObject);
    }
}
