using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerController : MonoBehaviour
{
    public int health;

    public virtual void Damage(int amount = 1)
    {
        health -= amount;
    }
}
