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

    [System.Serializable]
    public class Probability
    {
        public EffectType type;
        public float probability;
    }
    public Probability[] probabilities;

    // ******************************************* //

    [System.Serializable]
    public class CooldownEffectAttr
    {
        [Range(0f, 1f)]
        public float maxReduction;

        public AudioSource sound;
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

        public float selfMovementScale;
        public float opponentMovementScale;

        public AudioSource omae;
        public AudioSource nani;
        public AudioSource boom;
    }
    public ExplosionEffectAttr explosionAttributes;

    // ******************************************* //
    
    [System.Serializable]
    public class ThrowEffectAttr
    {
        public float minForce;
        public float maxForce;

        public AudioSource sound;
    }
    public ThrowEffectAttr throwAttributes;

    // ******************************************* //

    [System.Serializable]
    public class GravityEffectAttr
    {
        public float lowGravity;

        public float minDuration;
        public float maxDuration;

        public AudioSource sound;
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

        public AudioSource sound;
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

        public AudioSource beginAudio;
        public AudioSource endAudio;
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

        public AudioSource beginAudio;
        public AudioSource endAudio;
    }
    public SlowMoAttributes slowMoAttributes;

    // ******************************************* //

    [System.Serializable]
    public class FullLife3Attributes
    {
        public float visibleFor;
        public float fadeRate;

        public GameObject prefab;

        public AudioSource sound;
    }
    public FullLife3Attributes fullLife3Attributes;

    // ******************************************* //

    [System.Serializable]
    public class SizeAlterAttributes
    {
        public float minDuration;
        public float maxDuration;

        public float scale;

        public AudioSource sound;
    }
    public SizeAlterAttributes sizeAlterAttributes;

    // ******************************************* //

    [System.Serializable]
    public class PayWallAttributes
    {
        public float fadeRate;

        public float minDuration;
        public float maxDuration;

        public GameObject prefab;

        public AudioSource sound;
    }
    public PayWallAttributes payWallAttributes;
}
