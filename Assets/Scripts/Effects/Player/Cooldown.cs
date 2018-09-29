using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownEffect : Effect
{
    // Between 0 and 1
    private readonly float maxCooldownReduction = 0.8f;

    public override bool HasEnded()
    {
        return true;
    }

    public override void Begin(BasePlayerController player)
    {
        player.ReduceCooldown(EffectState.instance.CurrentIntensity * maxCooldownReduction);
    }
}
