using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Status
{
    public enum statusName { Bleed, Insinirate, Poison, Freeze};
    protected statusName name;
    protected float duration;
    protected float timer;
    protected float tick;
    protected float tickTimer;
    protected Sprite icon;
    protected int presision;
    public Status(float duration, float tick,  Sprite icon)
    {
        this.duration = duration;
        this.tick = tick;
        this.icon = icon;

        this.timer = 0;
        this.tickTimer = 0;
    }
        
    public bool resolveStatus(float deltaTime, HealthStatusManager HSman)
    {
        timer += deltaTime;
        if (timer > duration)
        {
            deltaTime -= timer - duration;
            if (deltaTime < 0)
                deltaTime = 0;
        }
        tickTimer += deltaTime;
        while (tickTimer >= tick || Mathf.Approximately(tickTimer, tick) )
        {
            tickTimer -= tick;
            //Debug.Log(deltaTime);
            efect(HSman);
        }
        if (timer > duration)
        {
            return true;
        }
        else
            return false;
    }
    public virtual void efect(HealthStatusManager HSman)
    {
        Debug.LogWarning("Not implemented Override");
    }
}