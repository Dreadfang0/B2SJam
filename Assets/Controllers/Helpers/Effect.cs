using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
{
    public abstract bool Ended();

    public virtual void Update() { }
}
