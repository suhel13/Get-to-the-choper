using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incinirate : Status
{
    public Incinirate(float duration, float tick) : base(duration, tick)
    {
        name = statusName.Insinirate;
    }

    public override void tickEfect(HealthStatusManager HSman)
    {
        Debug.Log("Incinirate efect trigeer");
    }
}