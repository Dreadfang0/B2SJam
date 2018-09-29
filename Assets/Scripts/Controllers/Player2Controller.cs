using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : BasePlayerController
{
    public float PowerMultiplier;
    public float speed;
    public float maxSpeed;
    public float jumpforce;
    public bool grounded;
    public Collider2D feetTrigger;
    
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
    private float[] cooldowns = new float[3];

    public override int Health
    {
        set
        {
            base.Health = value;

            // Play hurt sound etc.

            // Handle death here?
        }
    }

    public override void ReduceCooldown(float by)
    {
        var mult = 1f - by;

        for (int i = 0; i < cooldowns.Length; ++i)
            cooldowns[i] *= mult;
    }

    private void SetAbilityCooldown(Ability ability, float time)
    {
        cooldowns[(int)ability] = time;
    }

    private bool AbilityReady(Ability ability)
    {
        return cooldowns[(int)ability] <= 0f;
    }

    private void Update()
    {
        for (int i = 0; i < cooldowns.Length; ++i)
            cooldowns[i] = Mathf.Max(0f, cooldowns[i] - Time.deltaTime);
    }

    void FixedUpdate ()
    {
        if (rig.velocity.magnitude > maxSpeed)
        {
            rig.velocity = rig.velocity.normalized * maxSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rig.AddForce(Vector2.right * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rig.AddForce(Vector2.right * -speed);
        }
        if (Input.GetKeyDown(KeyCode.W) && grounded == true)
        {
            rig.AddForce(Vector2.up * jumpforce);
            grounded = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && AbilityReady(Ability.Gun))
        {
            Gun();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ground")
        {
            grounded = true;
        }
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
