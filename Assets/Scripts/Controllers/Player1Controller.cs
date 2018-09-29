using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : BasePlayerController
{
    // Gun ability stuff
    public Transform gunpoint;
    public GameObject bullet;

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

        if (Input.GetKeyDown(KeyCode.Alpha1) && AbilityReady(Ability.Ananas))
        {
            Ananas();
        }
    }

    void Ananas()
    {
        SetAbilityCooldown(Ability.Ananas, gunCooldown);
        var shot = (GameObject)Instantiate(bullet, gunpoint.position, gunpoint.rotation);
        shot.GetComponent<Rigidbody2D>().velocity = gunpoint.transform.right * 1 * Mathf.Sign(gunpoint.transform.localPosition.x);
        //Destroy(shot, 1.0f);
    }

    void DLC()
    {

    }

    void LootBoxStorm()
    {

    }
}
