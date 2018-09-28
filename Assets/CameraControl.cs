using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public float scrollSpeed = 1f;
    public GameObject[] players;
    public float catchUpPower = 2f;
    Camera mainCamera;
    // Use this for initialization
    void Awake () {
        mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        float catchUpSpeed = scrollSpeed;
        foreach (GameObject p in players)
        {
            //starts catchup from half screen upwards
            float lerp = mainCamera.WorldToScreenPoint(p.transform.position).y / (Screen.height / 2f);
            //raises to power to accelerate more towards the top of the screen
            if (lerp > 0 && scrollSpeed * Mathf.Pow(lerp,catchUpPower) > catchUpSpeed)
            {
                catchUpSpeed = scrollSpeed * Mathf.Pow(lerp, catchUpPower);
            }
        }
        mainCamera.transform.position += Vector3.up * Time.deltaTime * catchUpSpeed;
    }
}
