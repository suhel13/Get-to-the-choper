using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Status
{
    public enum statusName { Bleed, Insinirate, Poison, Freeze};
    public statusName name;
    public int id;
    protected float duration;
    protected float timer;
    protected float tick;
    protected float tickTimer;
    protected Sprite icon;
    public StatusIconControler statusIcon;
    protected int presision;
    public Status(float duration, float tick,  Sprite icon)
    {
        this.id = GameManager.Instance.nextStatusId();
        this.duration = duration;
        this.tick = tick;
        this.icon = icon;

        this.timer = 0;
        this.tickTimer = 0;
    }
    public Status (Status status, bool newID)
    {
        if(newID)
            this.id = GameManager.Instance.nextStatusId();
        else
            this.id = status.id;
        
        this.duration = status.duration;
        this.tick = status.tick;
        this.icon = status.icon;

        this.timer = 0;
        this.tickTimer = 0;
    }
    public virtual Status copy()
    {
        return null;
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
        while (tickTimer >= tick || Mathf.Approximately(tickTimer, tick) || Mathf.Abs(tick - tickTimer) < 0.0001 * tick)
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
    public void resetDuration()
    {
        timer = 0;
    }
    public void statusIconUpdate()
    {
        statusIcon.setFillAmount((duration - timer) / duration);
    }
    public virtual void efect(HealthStatusManager HSman)
    {
        Debug.LogWarning("Not implemented Override");
    }
}