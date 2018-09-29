using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public float pitchMultiplier;

	void Update ()
    {
        GetComponent<AudioSource>().pitch = Mathf.Clamp(1f - (1f - Time.timeScale) * pitchMultiplier, 0.5f, 2f);
	}
}
