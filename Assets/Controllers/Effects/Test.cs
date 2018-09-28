using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEffect : Effect
{
    private float timer = 0;

    public override bool Ended()
    {
        return timer >= 3f;
    }

    public override void Update()
    {
        timer += Time.deltaTime;
    }
}
