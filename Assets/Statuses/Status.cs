using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Status
{ 
    public enum statusName { Bleed, Insinirate, Poison, Frozen, Wet, Shock, Acid, Cold, Smoke, Push };
    public statusName name;
    public int id;
    protected float duration;
    protected float timer;
    protected float tick;
    protected float tickTimer;
    public StatusIconControler statusIcon;
    protected int presision;
    protected bool isStartEfectResolved;
    protected bool returnVal;
    protected List<int> statusesToRemove = new List<int>();
    protected List<Status> statusesToAdd = new List<Status>();


    public Status(float duration, float tick)
    {
        id = GameManager.Instance.nextStatusId();
        this.duration = duration;
        if (this.duration <= 0)
            this.duration = 0.01f;
        this.tick = tick;
        this.timer = 0;
        this.tickTimer = 0;
        this.isStartEfectResolved = false;
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
        this.isStartEfectResolved = false;
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
    public virtual void startEfect(HealthStatusManager HSman) //timing: in first frame after adding this status
    {   }
    public virtual void resolvePhysicsEfects(HealthStatusManager HSman) //timing: in fixed update
    {   }
    /// <summary>
    /// Check for Combinations with other elements on target.
    /// </summary>
    /// <returns>Returns a bool that specify if you want to add this status to target.</returns>
    public virtual bool resolveCombinations(HealthStatusManager HSman, Dictionary<int, Status> targetStatuses) //timing: before adding status
    { return true;  }

    public void resetStatus(Status newStatus)
    {
        switch (newStatus.name)
        {
            case statusName.Shock:
                resetShock(newStatus as Shock);
                break;

            default:
                Debug.Log("Default Reset");
                timer = 0;
                break;
        }
    }

    public virtual void resetShock(Shock newStatus)
    {   }
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