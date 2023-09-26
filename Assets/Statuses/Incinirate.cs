using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incinirate : Status
{
    public Incinirate(float duration, float tick, Sprite icon) : base(duration, tick, icon)
    {
        name = statusName.Insinirate;
    }

    public override void efect(HealthStatusManager HSman)
    {
        Debug.Log("Incinirate efect trigeer");
    }
}