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
        player.Push(Vector2.up * EffectAttributes.instance.throwAttributes.force);
    }
}
