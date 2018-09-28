using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
{
    public abstract bool HasEnded();

    public virtual void Begin(object player) { }

    public virtual void End() { }

    public virtual void Update() { }
}
