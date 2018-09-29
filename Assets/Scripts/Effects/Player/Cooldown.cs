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
        var attr = EffectAttributes.instance.cooldownAttributes;

        player.ReduceCooldown(EffectState.instance.CurrentIntensity * attr.maxReduction);

        attr.sound.Play();
    }
}
