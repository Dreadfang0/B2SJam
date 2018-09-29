using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : BasePlayerController
{
    // Pineapple ability stuff

    // Ability Cooldowns
    public float pineappleCooldown;
    public float dashCooldown;
    public float throwCooldown;

    private enum Ability
    {
        Pineapple,
        Dash,
        Throw,
    }

    public override int Health
    {
        set
        {
            base.Health = value;

            // Play hurt sound etc.

            // Handle death here?
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
