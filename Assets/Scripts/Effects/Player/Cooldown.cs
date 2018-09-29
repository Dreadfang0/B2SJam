using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownEffect : Effect
{
    public override bool HasEnded()
    {
        return true;
    }

    public override void Begin(BasePlayerController player)
    {
        player.ReduceCooldown(EffectState.instance.CurrentIntensity * EffectAttributes.instance.cooldownAttributes.maxReduction);
    }
}
