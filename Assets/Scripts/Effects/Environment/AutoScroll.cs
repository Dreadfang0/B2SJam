using UnityEngine;
using System.Collections;

public class AutoScrollEffect : Effect
{
    private bool ended = false;
    float duration = 5f;
    public override bool HasEnded()
    {
        return ended;
    }
    public override void Begin(BasePlayerController player)
    {
        CameraControl.instance.SetAutoScroll(true);
        EffectState.instance.StartCoroutine(
            HandleDuration(duration)
        );
        //speed too?
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
