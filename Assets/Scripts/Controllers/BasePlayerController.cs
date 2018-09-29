using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayerController : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float jumpforce;
    public float horizontalDamping;
    private bool grounded;
    public Collider2D feetTrigger;
    public Rigidbody2D rig;

    [SerializeField]
    private int health;

    [System.Serializable]
    public class KeyConfig
    {
        public KeyCode left;
        public KeyCode right;
        public KeyCode jump;
    }
    public KeyConfig keyConfig;

    private float[] cooldowns = new float[3];
    private bool frozen = false;
    protected bool facingRight = false;

    public BasePlayerController FindOpponent()
    {
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player != gameObject)
                return player.GetComponent<BasePlayerController>();
        }

        return null;
    }
    
    // 'by' is the multiplier for reduction.
    // For example the value 0.25 should reduce cooldown by a quarter
    public virtual void ReduceCooldown(float by)
    {
        var mult = 1f - by;

        for (int i = 0; i < cooldowns.Length; ++i)
            cooldowns[i] *= mult;
    }

    public void SetAbilityCooldown<T>(T ability, float time)
    {
        cooldowns[(int)(object)ability] = time;
    }

    public bool AbilityReady<T>(T ability)
    {
        return cooldowns[(int)(object)ability] <= 0f;
    }

    protected virtual void Update()
    {
        for (int i = 0; i < cooldowns.Length; ++i)
            cooldowns[i] = Mathf.Max(0f, cooldowns[i] - Time.deltaTime);
    }

    protected virtual void FixedUpdate()
    {
        if (Frozen)
            return;

        // Horizontal damping
        rig.velocity = new Vector2(
            rig.velocity.x * (1f - horizontalDamping * Time.deltaTime),
            rig.velocity.y
        );

        if (rig.velocity.magnitude > maxSpeed)
        {
            rig.velocity = rig.velocity.normalized * maxSpeed;
        }

        if (Input.GetKey(keyConfig.right))
        {
            transform.rotation = Quaternion.Euler(0f,0f,0f);

            
            rig.AddForce(Vector2.right * speed);
            facingRight = true;
        }
        if (Input.GetKey(keyConfig.left))
        {
            //transform.Rotate(Vector3.right * 180);
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            rig.AddForce(Vector2.right * -speed);
            facingRight = false;
        }
        if (Input.GetKeyDown(keyConfig.jump) && grounded == true)
        {
            rig.AddForce(Vector2.up * jumpforce);
            grounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ground")
        {
            grounded = true;
        }
    }

    public virtual int Health
    {
        get { return health; }
        set { health = value; }
    }

    public virtual bool Frozen
    {
        get { return frozen; }
        set {
            frozen = value;
            
            rig.constraints = frozen ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.FreezeRotation;
        }
    }

    // Dir is not normalized
    public virtual void Push(Vector2 dir)
    {
        // TODO: multiply by intensity
        rig.AddForce(dir);
    }
}
