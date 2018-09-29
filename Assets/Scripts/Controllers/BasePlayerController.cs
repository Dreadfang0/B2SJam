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
    public CapsuleCollider2D hitTrigger;

    [SerializeField]
    private int health;

    [System.Serializable]
    public class KeyConfig
    {
        public KeyCode left;
        public KeyCode right;
        public KeyCode jump;
        public KeyCode punch;
    }
    public KeyConfig keyConfig;

    private float[] cooldowns = new float[3];
    private bool frozen = false;
    private float movementScale = 1f;
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
            rig.velocity.x * (1f - horizontalDamping * (1f / movementScale) * Time.deltaTime),
            rig.velocity.y
        );

        rig.gravityScale = movementScale;

        if (rig.velocity.magnitude > maxSpeed)
        {
            rig.velocity = rig.velocity.normalized * maxSpeed;
        }

        if (Input.GetKey(keyConfig.right))
        {
            transform.rotation = Quaternion.Euler(0f,0f,0f);

            
            rig.AddForce(Vector2.right * speed * MovementScale);
            facingRight = true;
        }
        if (Input.GetKey(keyConfig.left))
        {
            //transform.Rotate(Vector3.right * 180);
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            rig.AddForce(Vector2.right * -speed * MovementScale);
            facingRight = false;
        }
        if (Input.GetKeyDown(keyConfig.jump) && grounded == true)
        {
            rig.AddForce(Vector2.up * jumpforce * MovementScale);
            grounded = false;
        }
        if (Input.GetKeyDown(keyConfig.punch))
        {
            StartCoroutine(Punch());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ground")
        {
            grounded = true;
        }
    }

    public float MovementScale
    {
        get { return movementScale; }
        set { movementScale = value; }
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
    bool punching = false;
    float punchTime = 0.25f;
    IEnumerator Punch()
    {
        punching = true;
        //start animating
        yield return new WaitForSeconds(punchTime);
        punching = false;
        Collider2D[] hits = Physics2D.OverlapCapsuleAll(hitTrigger.bounds.center, hitTrigger.bounds.size, CapsuleDirection2D.Horizontal, 0f);
        foreach (Collider2D c in hits)
        {
            if (c.isTrigger == false)
            {
                BasePlayerController hitPlayer = c.GetComponent<BasePlayerController>();
                if (hitPlayer != null && hitPlayer != this)
                {
                    Rigidbody2D rb = hitPlayer.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.AddForce(transform.right * 3 + transform.up * 3, ForceMode2D.Impulse);
                        break;
                    }
                }
            }
        }
    }
}
