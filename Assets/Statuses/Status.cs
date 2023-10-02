using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Status
{
    public enum statusName { Bleed, Insinirate, Poison, Freeze, Wet, Shock };
    public statusName name;
    public int id;
    protected float duration;
    protected float timer;
    protected float tick;
    protected float tickTimer;
    public StatusIconControler statusIcon;
    protected int presision;

    protected List<int> statusesToRemove = new List<int>();
    public Status(float duration, float tick)
    {
        this.id = GameManager.Instance.nextStatusId();
        this.duration = duration;
        if (this.duration <= 0)
            this.duration = 0.01f;
        this.tick = tick;
        this.timer = 0;
        this.tickTimer = 0;
    }
    public Status(Status status, bool newID)
    {
        if (newID)
            this.id = GameManager.Instance.nextStatusId();
        else
            this.id = status.id;

        this.duration = status.duration;
        if (this.duration <= 0)
            this.duration = 0.01f;
        this.tick = status.tick;

        this.timer = 0;
        this.tickTimer = 0;
    }
    public virtual Status copy()
    {
        return null;
    }
    public bool resolveStatus(float deltaTime, HealthStatusManager HSman)
    {
        normalEffect(HSman);

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
            tickEfect(HSman);
        }
        if (timer > duration)
        {
            return true;
        }
        else
            return false;
    }
    public virtual void resolvePhysicsEfects(HealthStatusManager HSman)
    {

    }
    public virtual void resolveCombinations(HealthStatusManager HSman, Dictionary<int, Status> targetStatuses)
    {

    }
    public void resetDuration()
    {
        timer = 0;
    }
    public void statusIconUpdate()
    {
        statusIcon.setFillAmount((duration - timer) / duration);
    }
    public virtual void normalEffect(HealthStatusManager HSman)
    {
        return;
    }
    public virtual void tickEfect(HealthStatusManager HSman)
    {
        return;
    }
}