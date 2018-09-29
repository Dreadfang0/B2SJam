using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : BasePlayerController
{
    // Gun ability stuff
    public Transform gunpoint;
    public GameObject ananas;

    // Ability Cooldowns
    public float gunCooldown;
    public float lootboxstormCooldown;
    public float dlcCooldown;

    private enum Ability
    {
        Ananas,
        Dash,
        Throw
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

        if (Input.GetKeyDown(KeyCode.RightControl) && AbilityReady(Ability.Ananas))
        {
            Ananas();
        }
    }

    void Ananas()
    {
        int direction = -1;
        if (facingRight)
        {
            direction = 1;
        }
        SetAbilityCooldown(Ability.Ananas, gunCooldown);
        GameObject shot = Instantiate(ananas, transform.position + Vector3.right * direction,Quaternion.identity);
        Rigidbody2D shotRb = shot.GetComponent<Rigidbody2D>();
        shotRb.velocity = transform.right * direction * 1f + transform.right * rig.velocity.x + transform.up * 3f;
        shotRb.angularVelocity = -500f * direction;
    }

    void DLC()
    {

    }

    void LootBoxStorm()
    {

    }
}
