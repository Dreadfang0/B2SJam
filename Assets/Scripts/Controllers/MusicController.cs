using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public float pitchMultiplier;

	void Update ()
    {
        GetComponent<AudioSource>().pitch = 1f - (1f - Time.timeScale) * pitchMultiplier;
	}
}
