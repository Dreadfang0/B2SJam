using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMoEffect : Effect
{
    public override bool HasEnded()
    {
        return true;
    }

    public override void Begin(BasePlayerController player)
    {
        var attr = EffectAttributes.instance.slowMoAttributes;
        
        var mind = attr.minDuration;
        var maxd = attr.maxDuration;
        var duration = mind + (maxd - mind) * EffectState.instance.CurrentIntensity;

        var mins = attr.minSpeed;
        var maxs = attr.maxSpeed;
        var speed = maxs - (maxs - mins) * EffectState.instance.CurrentIntensity;

        VersusState.instance.PushSpeedChange(speed, duration);
    }
}
