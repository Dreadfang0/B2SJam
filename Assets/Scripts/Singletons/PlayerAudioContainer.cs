using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioContainer : MonoBehaviour
{
    public static PlayerAudioContainer instance;

    void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);

        instance = this;
    }

    public AudioClip[] painSounds;
    public AudioClip[] punchSounds;
    public AudioClip[] deathSounds;
    public AudioClip jumpSound;
    public AudioClip runSound;
}
