using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeAlterEffect : Effect
{
    private bool ended = false;

    public override bool HasEnded()
    {
        return ended;
    }

    public override void Begin(BasePlayerController player)
    {
        EffectState.instance.StartCoroutine(HandleDuration(player));
    }

    private IEnumerator HandleDuration(BasePlayerController player)
    {
        var attr = EffectAttributes.instance.sizeAlterAttributes;
        var oldScale = player.transform.localScale;

        player.transform.localScale *= 0.5f;

        var min = attr.minDuration;
        var max = attr.maxDuration;

        attr.sound.Play();

        yield return new WaitForSeconds(min + (max - min) * EffectState.instance.CurrentIntensity);

        player.transform.localScale = oldScale;
        attr.sound.Play();

        ended = true;
    }
}
