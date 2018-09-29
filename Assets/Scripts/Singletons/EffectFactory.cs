using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    // Player
    Cooldown,
    Freeze,
    Throw,

    // Env

    
    __Count, // Keep this last
}

public class EffectFactory
{
    public static Effect Create(EffectType type)
    {
        // Return an effect instance of appropriate type
        switch (type)
        {
            case EffectType.Cooldown:
                return new CooldownEffect();

            case EffectType.Freeze:
                return new FreezeEffect();

            case EffectType.Throw:
                return new ThrowEffect();

            default:
                return null;
        }
    }
}
