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
    float lootboxstormCoolingdown;
    public float lootboxstormCooldown;
    float dlcCoolingdown;
    public float dlcCooldown;
    float gunCoolingdown;
    public float gunCooldown;

	// Use this for initialization
	void Start ()
    {
        
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

    // Update is called once per frame
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && gunCoolingdown == 0)
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
        gunCoolingdown = gunCooldown;
        var shot = (GameObject)Instantiate(bullet,gunpoint.position,gunpoint.rotation);
        shot.GetComponent<Rigidbody2D>().velocity = gunpoint.transform.right * 10;
        Destroy(shot, 1.0f);
    }
    void DLC()
    {

    }
    void lootBoxStorm()
    {

    }
}
