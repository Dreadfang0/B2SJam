using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : Effect
{
    private bool ended = false;

    public override bool HasEnded()
    {
        return ended;
    }

    public override void Begin(BasePlayerController player)
    {
        EffectState.instance.StartCoroutine(ExplosionRoutine(player, player.FindOpponent()));
    }

    private IEnumerator ExplosionRoutine(BasePlayerController self, BasePlayerController opponent)
    {
        self.Frozen = true;
        opponent.Frozen = true;

        var attr = EffectAttributes.instance.explosionAttributes;

        attr.omae.Play();

        yield return new WaitForSecondsRealtime(attr.omae.clip.length);

        attr.nani.Play();

        yield return new WaitForSecondsRealtime(attr.nani.clip.length);

        attr.boom.Play();

        var explosionDelay = 0.85f;

        yield return new WaitForSecondsRealtime(explosionDelay);

        self.Frozen = false;
        opponent.Frozen = false;

        var pushDir = opponent.gameObject.transform.position - self.gameObject.transform.position;
        var min = EffectAttributes.instance.explosionAttributes.minForce;
        var max = EffectAttributes.instance.explosionAttributes.maxForce;
        var pushForce = (min + (max - min) * EffectState.instance.CurrentIntensity);

        self.Push(Vector2.up * pushForce);
        opponent.Push(pushDir.normalized * pushForce);

        yield return new WaitForSecondsRealtime(attr.boom.clip.length - explosionDelay);

        ended = true;
    }
}
