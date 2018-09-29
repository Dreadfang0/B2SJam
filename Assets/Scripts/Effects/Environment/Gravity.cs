using UnityEngine;
using System.Collections;

public class Gravity:Effect
{
    float defaultGravity = -9.81f;
    float lowGravity = -6f;
    bool ended = false;
    float duration = 5f;
    public override bool HasEnded()
    {
        return ended;
    }
    public override void Begin(BasePlayerController player)
    {
        Physics2D.gravity = Vector2.down* lowGravity;
        EffectState.instance.StartCoroutine(
            HandleDuration(duration * EffectState.instance.CurrentIntensity)
        );
    }
    public override void End()
    {
        Physics2D.gravity = Vector2.down * defaultGravity;
    }
    private IEnumerator HandleDuration(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ended = true;
    }
}
