using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class VersusUI : MonoBehaviour
{
    public static VersusUI instance;

    public BasePlayerController[] players;

    public Text[] playerHealthTexts;
    public Text[] playerCooldowns;

    void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);

        instance = this;
    }

    private void Update()
    {
        for (int i = 0; i < playerHealthTexts.Length; ++i)
        {
            playerHealthTexts[i].text = players[i].Health.ToString();
        }

        var numCooldowns = playerCooldowns.Length / players.Length;

        for (int i = 0; i < playerCooldowns.Length; ++i)
        {
            var cooldown = players[i / numCooldowns].GetRemainingCooldown(i % numCooldowns);
            string text;

            if (cooldown > 0f)
                text = ((int)cooldown + 1).ToString();
            else
                text = "Ready!";

            playerCooldowns[i].text = text;
        }
    }
}
