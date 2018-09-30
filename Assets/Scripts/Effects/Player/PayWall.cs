using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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

        player.Frozen = player.FindOpponent().Frozen = true;

        var obj = GameObject.Instantiate(attr.prefab, GameObject.Find("Canvas").transform);
        var sprite = obj.GetComponent<Image>();

        for (var color = sprite.color; color.a < 1f; color.a += attr.fadeRate)
        {
            sprite.color = color;
            yield return null;
        }

        var min = attr.minDuration;
        var max = attr.maxDuration;

        attr.sound.Play();

        yield return new WaitForSeconds(min + (max - min) * EffectState.instance.CurrentIntensity);

        player.Frozen = player.FindOpponent().Frozen = false;

        for (var color = sprite.color; color.a > 0f; color.a -= attr.fadeRate)
        {
            sprite.color = color;
            yield return null;
        }

        GameObject.Destroy(obj);

        ended = true;
    }
}
