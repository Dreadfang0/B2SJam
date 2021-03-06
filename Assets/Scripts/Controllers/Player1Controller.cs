﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : BasePlayerController
{
    // Audio
    public AudioClip dashSound1;
    public AudioClip dashSound2;

    public AudioClip throwSound;
    public AudioClip boomSound;

    // Gun ability stuff
    public Transform gunpoint;
    public GameObject ananas;

    // Ability Cooldowns
    public float ananasCooldown;
    public float dashCooldown;
    public float grabCooldown;
    bool grabbing = false;
    public Vector3 punchVector;
    public KeyCode[] abilityKeys;
    private enum Ability
    {
        Ananas,
        Dash,
        Grab
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
    private void Start()
    {
        anim.SetInteger("player", 1);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Input.GetKeyDown(abilityKeys[0]) && AbilityReady(Ability.Ananas))
        {
            Ananas();
        }
        if (Input.GetKeyDown(abilityKeys[1]) && AbilityReady(Ability.Dash))
        {
            Dash();
        }
        if (Input.GetKeyDown(abilityKeys[2]) && AbilityReady(Ability.Grab))
        {
            StartCoroutine(Grab());
        }
    }
    /*protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.RightControl) && AbilityReady(Ability.Ananas))
        {
            Ananas();
        }
        if (Input.GetKeyDown(KeyCode.Minus) && AbilityReady(Ability.Dash))
        {
            Dash();
        }
        if (Input.GetKeyDown(KeyCode.RightAlt) && AbilityReady(Ability.Grab))
        {
            StartCoroutine(Grab());
        }
    }*/
    void Ananas()
    {
        StartCoroutine(PlaySound(throwSound));

        anim.SetInteger("AnimParameter", 4);
        SetAbilityCooldown(Ability.Ananas, ananasCooldown);
        GameObject shot = Instantiate(ananas, gunpoint.position,Quaternion.identity);
        Rigidbody2D shotRb = shot.GetComponent<Rigidbody2D>();
        shotRb.velocity = transform.right * 1f + transform.right * Mathf.Abs(rig.velocity.x) + transform.up * 3f;
        shotRb.angularVelocity = -500f * Direction;
    }
    int Direction {
        get {
            int direction = -1;
            if (facingRight)
            {
                direction = 1;
            }
            return direction;
        }
    }
    void Dash()
    {
        StartCoroutine(PlaySound(dashSound1));
        SetAbilityCooldown(Ability.Dash,dashCooldown);
        anim.SetInteger("AnimParameter", 5);
        rig.AddForce(transform.right * 500f);
        //animation
    }
    IEnumerator Grab()
    {
        StartCoroutine(PlaySound(dashSound2));
        rig.AddForce(transform.right * 500f);
        //grabdash animation
        anim.SetInteger("AnimParameter", 6);
        grabbing = true;
        yield return new WaitForSeconds(.5f);

        grabbing = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (grabbing)
        {
                Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
                if (rb != null)
                {

                    PlayPunchSound();
                    //rb.AddForce(transform.right * 10f + transform.up * 30f,ForceMode2D.Impulse);
                    rb.AddForce(transform.right * punchVector.x + transform.up * punchVector.y, ForceMode2D.Impulse);
                    //sound effect & animation for punch
                }
        }
    }
}
