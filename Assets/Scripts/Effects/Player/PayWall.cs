using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayWallEffect : Effect
{
    private bool ended = false;

    public override bool HasEnded()
    {
        return ended;
    }

    public override void Begin(BasePlayerController player)
    {
        EffectState.instance.StartCoroutine(HandleDuration(player));
    }

    private IEnumerator HandleDuration(BasePlayerController player)
    {
        var attr = EffectAttributes.instance.payWallAttributes;

        attr.sound.Play();

        var obj = GameObject.Instantiate(attr.prefab, player.transform);
        var text = obj.GetComponent<TextMesh>();

        for (var color = text.color; color.a < 1f; color.a += attr.fadeRate)
        {
            text.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(attr.visibleFor);

        for (var color = text.color; color.a > 0f; color.a -= attr.fadeRate)
        {
            text.color = color;
            yield return null;
        }

        GameObject.Destroy(obj);

        ended = true;
    }
}
