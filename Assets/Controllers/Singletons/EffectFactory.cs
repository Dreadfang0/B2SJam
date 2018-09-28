using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    Test,
    __Count,
}

public class EffectFactory
{
    public static Effect Create(EffectType type)
    {
        // Return an effect instance of appropriate type
        switch (type)
        {
            case EffectType.Test:
                return new TestEffect();

            default:
                return null;
        }
    }
}
