using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectListener
{
    public virtual void Begin(Effect effect) { }

    public virtual void End(Effect effect) { }
}
