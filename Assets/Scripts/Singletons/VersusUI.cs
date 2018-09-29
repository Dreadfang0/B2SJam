using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersusUI : MonoBehaviour
{
    public static VersusUI instance;

    void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);

        instance = this;
    }
}
