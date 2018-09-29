using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectState : MonoBehaviour
{
    public static EffectState instance;

    // Effect test override (set to __Count to disable)
    public EffectType effectTypeOverride = EffectType.__Count;

    private float currentIntensity = 0.1f;
    private Dictionary<EffectType, List<Effect>> activeEffects = new Dictionary<EffectType, List<Effect>>();
    // private Dictionary<EffectType, List<EffectListener>> effectListeners = new Dictionary<EffectType, List<EffectListener>>();
    
	void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);

        instance = this;
    }

    void Update()
    {
        foreach (var list in activeEffects)
        {
            foreach (var effect in list.Value)
            {
                effect.Update();

                if (effect.HasEnded())
                {
                    effect.End();

                    //foreach (var listener in effectListeners[list.Key])
                    //    listener.End(effect);
                }
            }

            list.Value.RemoveAll(effect => effect.HasEnded());
        }
    }

    public float CurrentIntensity
    {
        get { return currentIntensity; }
    }

    public void RaiseIntensity(float by)
    {
        currentIntensity = Mathf.Max(currentIntensity + by, 1f);
    }

    //public void AddListener(EffectType type, EffectListener listener)
    //{
    //    effectListeners[type].Add(listener);
    //}

    public void Trigger(EffectType type, BasePlayerController player = null)
    {
        var effect = EffectFactory.Create(type);

        Debug.Assert(effect != null, "EffectFactory returned null with type " + type);

        //foreach (var listener in effectListeners[type])
        //    listener.Begin(effect);

        effect.Begin(player);

        activeEffects[type].Add(effect);
    }

    // Trigger a random effect
    public void Trigger(BasePlayerController player = null)
    {
        Trigger(
            effectTypeOverride != EffectType.__Count ?
                effectTypeOverride :
                (EffectType)Random.Range(0, (int)(EffectType.__Count)),
            player
        );
    }
}
