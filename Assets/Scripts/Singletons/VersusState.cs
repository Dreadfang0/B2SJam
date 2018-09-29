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

    public float timeShiftEaseTime = 0.5f;

    private class SpeedChange
    {
        public float targetSpeed;
        public float duration;
        public float progress = 0f;
    }
    private List<SpeedChange> speedChangeStack = new List<SpeedChange>();

    void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);

        instance = this;
    }

    private void Start()
    {
        StartCoroutine(RaiseIntensity());
        StartCoroutine(HandleSpeedChanges());
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

    public void PushSpeedChange(float targetSpeed, float duration)
    {
        var change = new SpeedChange
        {
            targetSpeed = targetSpeed,
            duration = duration
        };

        speedChangeStack.Add(change);
    }

    private IEnumerator HandleSpeedChanges()
    {
        while (true)
        {
            foreach (var change in speedChangeStack)
                change.progress += Time.unscaledDeltaTime;

            speedChangeStack.RemoveAll(change => change.progress >= change.duration);

            if (speedChangeStack.Count != 0)
            {
                float easeTime = timeShiftEaseTime;
                var lastIndex = speedChangeStack.Count - 1;
                var top = speedChangeStack[lastIndex];
                var baseSpeed = speedChangeStack.Count > 1 ? speedChangeStack[lastIndex - 1].targetSpeed : 1f;

                if (top.progress < easeTime)
                {
                    Time.timeScale = Mathf.Lerp(baseSpeed, top.targetSpeed, top.progress / easeTime);
                }
                else if (top.progress < (top.duration - easeTime))
                {
                    Time.timeScale = top.targetSpeed;
                }
                else if (top.progress < top.duration)
                {
                    Time.timeScale = Mathf.Lerp(top.targetSpeed, baseSpeed, (easeTime - (top.duration - top.progress)) / easeTime);
                }
            }

            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        }
    }
}
