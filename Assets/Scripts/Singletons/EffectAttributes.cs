using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAttributes : MonoBehaviour
{
    public static EffectAttributes instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void OnValidate()
    {
        freezeAttributes.maxDuration = Mathf.Max(0f, freezeAttributes.maxDuration);
    }

    // ******************************************* //

    [System.Serializable]
    public class CooldownEffectAttr
    {
        [Range(0f, 1f)]
        public float maxReduction;
    }
    public CooldownEffectAttr cooldownAttributes;

    // ******************************************* //

    [System.Serializable]
    public class FreezeEffectAttr
    {
        public float minDuration;
        public float maxDuration;

        public AudioSource beginAudio;
    }
    public FreezeEffectAttr freezeAttributes;
    
    // ******************************************* //

    [System.Serializable]
    public class ExplosionEffectAttr
    {
        public float minForce;
        public float maxForce;
    }
    public ExplosionEffectAttr explosionAttributes;

    // ******************************************* //
    
    [System.Serializable]
    public class ThrowEffectAttr
    {
        public float minForce;
        public float maxForce;
    }
    public ThrowEffectAttr throwAttributes;

    // ******************************************* //

    [System.Serializable]
    public class GravityEffectAttr
    {
        public float lowGravity;

        public float minDuration;
        public float maxDuration;
    }
    public GravityEffectAttr gravityAttributes;

    // ******************************************* //

    [System.Serializable]
    public class AutoScrollAttributes
    {
        public float minDuration;
        public float maxDuration;

        public float minSpeed;
        public float maxSpeed;
    }
    public AutoScrollAttributes autoScrollAttributes;

    // ******************************************* //

    [System.Serializable]
    public class SpeedUpAttributes
    {
        public float minDuration;
        public float maxDuration;

        public float minSpeed;
        public float maxSpeed;
    }
    public SpeedUpAttributes speedUpAttributes;

    // ******************************************* //

    [System.Serializable]
    public class SlowMoAttributes
    {
        public float minDuration;
        public float maxDuration;

        public float minSpeed;
        public float maxSpeed;
    }
    public SlowMoAttributes slowMoAttributes;
}
