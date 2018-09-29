using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayerController : MonoBehaviour
{
    public Rigidbody2D rig;

    [SerializeField]
    private int health;

    private bool frozen = false;

    public BasePlayerController FindOpponent()
    {
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player != gameObject)
                return player.GetComponent<BasePlayerController>();
        }

        return null;
    }

    public virtual int Health
    {
        get { return health; }
        set { health = value; }
    }

    public virtual bool Frozen
    {
        get { return frozen; }
        set { frozen = value; }
    }

    // Dir is not normalized
    public virtual void Push(Vector2 dir)
    {
        // TODO: multiply by intensity
        rig.AddForce(dir);
    }

    // 'by' is the multiplier for reduction.
    // For example the value 0.25 should reduce cooldown by a quarter
    public virtual void ReduceCooldown(float by) { }
}
