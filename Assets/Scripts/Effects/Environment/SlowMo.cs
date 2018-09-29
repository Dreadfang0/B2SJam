using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMoEffect : Effect
{
    private bool ended = false;

    public override bool HasEnded()
    {
        return ended;
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

        EffectState.instance.StartCoroutine(HandleDuration(duration));
    }

    private IEnumerator HandleDuration(float duration)
    {
        var attr = EffectAttributes.instance.slowMoAttributes;

        attr.beginAudio.Play();

        yield return new WaitForSecondsRealtime(duration + attr.endAudio.clip.length / 2);

        attr.endAudio.Play();
    }
}
