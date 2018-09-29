using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowEffect : Effect
{
    private readonly float throwForce = 1000f;

    public override bool HasEnded()
    {
        return true;
    }

    public override void Begin(BasePlayerController player)
    {
        player.Push(Vector2.up * throwForce);
    }
}
