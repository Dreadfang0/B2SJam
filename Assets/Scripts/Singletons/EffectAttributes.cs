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
        public float maxDuration;
    }
    public FreezeEffectAttr freezeAttributes;
    
    // ******************************************* //

    [System.Serializable]
    public class ExplosionEffectAttr
    {
        public float pushForce;
    }
    public ExplosionEffectAttr explosionAttributes;

    // ******************************************* //
    
    [System.Serializable]
    public class ThrowEffectAttr
    {
        public float force;
    }
    public ThrowEffectAttr throwAttributes;
}
