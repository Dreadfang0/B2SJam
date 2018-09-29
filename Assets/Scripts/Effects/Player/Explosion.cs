﻿using System.Collections;
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

        yield return new WaitForSeconds(0f); // placeholder

        // 1. Zoom to self -> omae wa mou shindeiru
        // 2. Pan to opponent -> NANI?!
        // 3. xplosion

        var pushDir = opponent.gameObject.transform.position - self.gameObject.transform.position;
        var min = EffectAttributes.instance.explosionAttributes.minForce;
        var max = EffectAttributes.instance.explosionAttributes.maxForce;

        opponent.Push(pushDir.normalized * (min + (max - min) * EffectState.instance.CurrentIntensity));

        self.Frozen = false;
        opponent.Frozen = false;

        ended = true;
    }
}
