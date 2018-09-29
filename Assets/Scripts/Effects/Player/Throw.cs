using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowEffect : Effect
{
    public override bool HasEnded()
    {
        return true;
    }

    public override void Begin(BasePlayerController player)
    {
        var min = EffectAttributes.instance.throwAttributes.minForce;
        var max = EffectAttributes.instance.throwAttributes.maxForce;

        player.Push(Vector2.up * (min + (max - min) * EffectState.instance.CurrentIntensity));

        EffectAttributes.instance.throwAttributes.sound.Play();
    }
}
