using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullLife3Effect : Effect
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
        var attr = EffectAttributes.instance.fullLife3Attributes;

        attr.sound.Play();

        var obj = GameObject.Instantiate(attr.prefab, player.transform);
        var sprite = obj.GetComponent<SpriteRenderer>();

        for (var color = sprite.color; color.a < 1f; color.a += 0.01f)
        {
            sprite.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(attr.visibleFor);

        for (var color = sprite.color; color.a > 0f; color.a -= 0.01f)
        {
            sprite.color = color;
            yield return null;
        }

        attr.sound.Stop();

        ended = true;
    }
}
