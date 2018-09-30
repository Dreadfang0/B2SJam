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
        var attr = EffectAttributes.instance.explosionAttributes;

        self.MovementScale = attr.selfMovementScale;
        opponent.MovementScale = attr.opponentMovementScale;

        attr.omae.Play();
        SpriteRenderer kolor = self.GetComponentInChildren<SpriteRenderer>();
        kolor.material.SetColor("_Color",Color.yellow);
        yield return new WaitForSecondsRealtime(attr.omae.clip.length);
        kolor.material.SetColor("_Color", Color.magenta);
        attr.nani.Play();

        yield return new WaitForSecondsRealtime(attr.nani.clip.length);
        kolor.material.SetColor("_Color", Color.red);
        attr.boom.Play();

        var explosionDelay = 0.85f;

        yield return new WaitForSecondsRealtime(explosionDelay);
        
        self.MovementScale = opponent.MovementScale = 1f;

        var pushDir = opponent.gameObject.transform.position - self.gameObject.transform.position;
        var min = EffectAttributes.instance.explosionAttributes.minForce;
        var max = EffectAttributes.instance.explosionAttributes.maxForce;
        var pushForce = (min + (max - min) * EffectState.instance.CurrentIntensity);
        kolor.material.SetColor("_Color", Color.white);
        self.Push(Vector2.up * pushForce);
        opponent.Push(pushDir.normalized * pushForce);
        GameObject InstantiatedFX = GameObject.Instantiate(attr.explosionFX, self.transform.position, Quaternion.identity);
        GameObject.Destroy(InstantiatedFX, 1f);
        
        yield return new WaitForSecondsRealtime(attr.boom.clip.length - explosionDelay);

        ended = true;
    }
}
