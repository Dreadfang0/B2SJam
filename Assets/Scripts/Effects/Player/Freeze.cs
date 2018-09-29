using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEffect : Effect
{
    private readonly float maxFreezeDuration = 3f;

    private bool ended = false;
    private BasePlayerController player;

    public override bool HasEnded()
    {
        return ended;
    }

    public override void Begin(BasePlayerController player)
    {
        this.player = player;

        player.Frozen = true;

        EffectState.instance.StartCoroutine(
            HandleDuration(maxFreezeDuration * EffectState.instance.CurrentIntensity)
        );
    }

    public override void End()
    {
        player.Frozen = false;
    }

    private IEnumerator HandleDuration(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ended = true;
    }
}
