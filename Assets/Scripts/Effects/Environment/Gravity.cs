using UnityEngine;
using System.Collections;

public class GravityEffect:Effect
{
    float oldGravity;

    bool ended = false;

    public override bool HasEnded()
    {
        return ended;
    }

    public override void Begin(BasePlayerController player)
    {
        oldGravity = Physics2D.gravity.y;

        Physics2D.gravity = Vector2.down * EffectAttributes.instance.gravityAttributes.lowGravity;

        var min = EffectAttributes.instance.gravityAttributes.minDuration;
        var max = EffectAttributes.instance.gravityAttributes.maxDuration;

        EffectState.instance.StartCoroutine(
            HandleDuration(min + (max - min) * EffectState.instance.CurrentIntensity)
        );
    }

    public override void End()
    {
        Physics2D.gravity = Vector2.down * oldGravity;
    }

    private IEnumerator HandleDuration(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ended = true;
    }
}
