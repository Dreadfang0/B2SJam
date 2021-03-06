﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    // Player
    Cooldown,
    Freeze,
    Throw,
    Explosion,
    FullLife3,
    SizeAlter,
    PayWall,

    // Env
    Gravity,
    AutoScroll,
    SlowMo,
    SpeedUp,

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

            case EffectType.Explosion:
                return new ExplosionEffect();

            case EffectType.FullLife3:
                return new FullLife3Effect();

            case EffectType.SizeAlter:
                return new SizeAlterEffect();

            case EffectType.PayWall:
                return new PayWallEffect();

            case EffectType.Gravity:
                return new GravityEffect();

            case EffectType.AutoScroll:
                return new AutoScrollEffect();

            case EffectType.SlowMo:
                return new SlowMoEffect();

            case EffectType.SpeedUp:
                return new SpeedUpEffect();

            default:
                return null;
        }
    }
}
