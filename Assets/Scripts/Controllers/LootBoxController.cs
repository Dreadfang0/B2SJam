using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LootBoxController : MonoBehaviour
{
    public GameObject UIPopupPrefab;
    bool triggered = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        BasePlayerController player = col.GetComponent<BasePlayerController>();
        if (player != null && !triggered)
        {
            triggered = true;
            StartCoroutine(OnPickup(player));
        }
    }

    private IEnumerator OnPickup(BasePlayerController player)
    {
        var type = EffectState.instance.Trigger(player);

        StartCoroutine(DoPopup(type));

        var sound = GetComponent<AudioSource>();

        GetComponent<SpriteRenderer>().enabled = false;
        sound.Play();

        yield return new WaitForSeconds(sound.clip.length);

        Destroy(gameObject);
    }

    private IEnumerator DoPopup(EffectType type)
    {
        var sprite = EffectAttributes.instance.effectImages[(int)type];

        if (sprite != null)
        {
            var canvas = GameObject.Find("Canvas");

            var obj = Instantiate(UIPopupPrefab, canvas.transform);
            var duration = EffectAttributes.instance.effectImageDuration;
            var fadeRate = EffectAttributes.instance.effectImageFadeRate;
            var image = obj.GetComponent<Image>();

            image.sprite = sprite;

            for (var color = image.color; color.a < 1f; color.a += fadeRate)
            {
                image.color = color;
                yield return null;
            }

            yield return new WaitForSecondsRealtime(duration);

            for (var color = image.color; color.a > 0f; color.a -= fadeRate)
            {
                image.color = color;
                yield return null;
            }

            Destroy(obj);
        }
    }
}
