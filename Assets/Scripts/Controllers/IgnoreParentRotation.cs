using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreParentRotation : MonoBehaviour
{
	void LateUpdate ()
    {
        transform.rotation = Quaternion.identity;
	}
}
