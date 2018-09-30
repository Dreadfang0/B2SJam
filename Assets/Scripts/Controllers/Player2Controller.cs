using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : BasePlayerController
{
    public AudioClip teleportBeginSound;
    public AudioClip teleportEndSound;

    public AudioClip miniLootboxSound;

    public AudioClip gunSound;

    // Gun ability stuff
    public Transform gunpoint;
    public GameObject bullet;

    // DLC ability stuff
    public Transform Telepoint;
    public float teleportTime;
    public CapsuleCollider2D hitbox;
    public SpriteRenderer visuals; // <-- Replace meshrenderer with Sprite renderer when art is gotten
    public ParticleSystem telePart; // <-- make a fancy particle effect to be activated during teleportation sequence
    // Lootbox Storm ability stuff (use gunpoint to shoot the stuff)
    public GameObject minilootbox;
    public float minimumShootAngle;
    public float maximumShootAngle;
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
    private void Start()
    {
        anim.SetInteger("player", 2);
        telePart.Stop();
    }
    protected override void FixedUpdate ()
    {
        base.FixedUpdate();
   
        if (Input.GetKeyDown(KeyCode.Alpha1) && AbilityReady(Ability.Gun))
        {
            Gun();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && AbilityReady(Ability.Dlc))
        {
            StartCoroutine(teleTime());
            SetAbilityCooldown(Ability.Dlc, dlcCooldown);

        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && AbilityReady(Ability.Storm))
        {
            StartCoroutine(storm());
            SetAbilityCooldown(Ability.Storm, lootboxstormCooldown);
        }
    }

    void Gun()
    {
        StartCoroutine(PlaySound(gunSound));

        SetAbilityCooldown(Ability.Gun, gunCooldown);
        anim.SetInteger("AnimParameter", 4);
        var shot = (GameObject)Instantiate(bullet, gunpoint.position, gunpoint.rotation);
        shot.GetComponent<Rigidbody2D>().velocity = gunpoint.transform.right * 15 * Mathf.Sign(gunpoint.transform.localPosition.x);
        Destroy(shot, 0.3f);
    }

    void DLC()
    {
        
        
    }
    IEnumerator teleTime()
    {
        StartCoroutine(PlaySound(teleportBeginSound));

        anim.SetInteger("AnimParameter", 5);
        yield return new WaitForSeconds(teleportTime);
        visuals.enabled = false;
        hitbox.enabled = false;
        rig.gravityScale = 0;
        telePart.Play();
        yield return new WaitForSeconds(teleportTime);
        gameObject.transform.position = Telepoint.position;
        yield return new WaitForSeconds(teleportTime);
        StartCoroutine(PlaySound(teleportEndSound));
        telePart.Stop();
        visuals.enabled = true;
        hitbox.enabled = true;
        rig.gravityScale = 1;
    }
    void LootBoxStorm()
    {
        SetAbilityCooldown(Ability.Storm, lootboxstormCooldown);
        /*
        for (int i = 0; i < 10; i++)
        {
            var shot = (GameObject)Instantiate(minilootbox, gunpoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + Random.Range(minimumShootAngle, maximumShootAngle))) as GameObject;
            Vector3 direction = minilootbox.transform.rotation * Vector3.right;


            shot.GetComponent<Rigidbody2D>().AddRelativeForce(direction * 300);

            Destroy(shot, 1.0f);
            
        }*/
        
    }
    IEnumerator storm()
    {
        for (int i = 0; i < 10; i++)
        {
            anim.SetInteger("AnimParameter", 6);
            yield return new WaitForSeconds(0.1f);
            var shot = (GameObject)Instantiate(minilootbox, gunpoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + Random.Range(minimumShootAngle, maximumShootAngle))) as GameObject;
            Vector3 direction = minilootbox.transform.rotation * transform.right;

            StartCoroutine(PlaySound(miniLootboxSound));

            shot.GetComponent<Rigidbody2D>().AddRelativeForce(direction * 2000);

            Destroy(shot, 1.0f);
            
        }
        
    }
}
