using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersusState : MonoBehaviour
{
    public static VersusState instance;

    // Thresholds in seconds
    // Intensity stays at minimum until lower threshold is passed
    // Intensity will reach maximum at upper threshold
    public int intensityLowerThreshold;
    public int intensityUpperThreshold;

    void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);

        instance = this;
    }

    private void Start()
    {
        StartCoroutine(RaiseIntensity());
    }

    private IEnumerator RaiseIntensity()
    {
        yield return new WaitForSeconds(intensityLowerThreshold);

        var eff = EffectState.instance;
        var secondsToMax = intensityUpperThreshold - intensityLowerThreshold;
        var intensityRaise = (1f - eff.initialIntensity) / secondsToMax;

        while (eff.CurrentIntensity < 1f)
        {
            eff.RaiseIntensity(intensityRaise);
            yield return new WaitForSeconds(1);
        }
    }
}
