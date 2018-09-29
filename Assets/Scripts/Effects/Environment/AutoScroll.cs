using UnityEngine;
using System.Collections;

public class AutoScrollEffect : Effect
{
    private bool ended = false;

    public override bool HasEnded()
    {
        return ended;
    }

    public override void Begin(BasePlayerController player)
    {
        {
            var min = EffectAttributes.instance.autoScrollAttributes.minSpeed;
            var max = EffectAttributes.instance.autoScrollAttributes.maxSpeed;

            // TODO: SetAutoScroll accept a float
            CameraControl.instance.SetAutoScroll(true /*min + (max - min) * EffectState.instance.CurrentIntensity*/);
        }

        {
            var min = EffectAttributes.instance.autoScrollAttributes.minDuration;
            var max = EffectAttributes.instance.autoScrollAttributes.maxDuration;

            EffectState.instance.StartCoroutine(
                HandleDuration(min + (max - min) * EffectState.instance.CurrentIntensity)
            );
        }
    }

    public override void End()
    {
        CameraControl.instance.SetAutoScroll(false);
    }

    private IEnumerator HandleDuration(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ended = true;
    }
}
