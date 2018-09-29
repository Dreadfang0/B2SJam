using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : BasePlayerController
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
        Gun,
        Dlc,
        Storm,
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

        if (Input.GetKeyDown(KeyCode.Alpha1) && AbilityReady(Ability.Gun))
            Gun();
    }

    void Gun()
    {
        SetAbilityCooldown(Ability.Gun, gunCooldown);
        var shot = (GameObject)Instantiate(bullet, gunpoint.position, gunpoint.rotation);
        shot.GetComponent<Rigidbody2D>().velocity = gunpoint.transform.right * 10;
        Destroy(shot, 1.0f);
    }

    void DLC()
    {

    }

    void LootBoxStorm()
    {

    }
}
