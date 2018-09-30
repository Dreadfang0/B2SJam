using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxController : MonoBehaviour
{
    bool triggered = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        BasePlayerController player = col.GetComponent<BasePlayerController>();
        if (player != null && !triggered)
        {
            triggered = true;
            StartCoroutine(OnPickup(player));
        }
    }

    private IEnumerator OnPickup(BasePlayerController player)
    {
        EffectState.instance.Trigger(player);

        var sound = GetComponent<AudioSource>();

        GetComponent<SpriteRenderer>().enabled = false;
        sound.Play();

        yield return new WaitForSeconds(sound.clip.length);

        // TODO: play animation?

        Destroy(gameObject);
    }
}
