using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl instance;
    public float scrollSpeed = 1f;
    public GameObject[] players;
    public float catchUpPower = 2f;
    Camera mainCamera;
    public bool autoScroll = false;
    public float autoScrollSpeed = 1f;
    public float catchUpTreshold = 0.75f;
    // Use this for initialization
    void Awake () {
        mainCamera = Camera.main;
        instance = this;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float catchUpSpeed = scrollSpeed;
        if (autoScroll == false)
        {
            catchUpSpeed = 0f;
        }
        foreach (GameObject p in players)
        {
            float lerp = mainCamera.WorldToScreenPoint(p.transform.position).y / Screen.height;
            //lerp to speed up towards the top of the screen
            if (lerp > catchUpTreshold && scrollSpeed * Mathf.Lerp(0,catchUpPower,lerp - catchUpTreshold)> catchUpSpeed)
            {
                catchUpSpeed = scrollSpeed * Mathf.Lerp(0, catchUpPower, lerp - catchUpTreshold);
            }
        }
            mainCamera.transform.position += Vector3.up * Time.deltaTime * (catchUpSpeed * autoScrollSpeed);
    }

    public void SetAutoScroll(bool enabled, float speed) {
        autoScroll = enabled;
        autoScrollSpeed = speed;
    }
}
