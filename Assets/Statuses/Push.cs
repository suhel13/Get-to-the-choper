using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : Status
{
    Vector2 dir;
    float maxSpeed;
    float minSpeed;
    [HideInInspector] public Vector2 actualSpeed;
    public float pushFallOff;

    public Push(Push push, bool newID) : base(push, newID)
    {
        dir = push.dir;
        maxSpeed = push.maxSpeed;
        minSpeed = push.minSpeed;
        pushFallOff = push.pushFallOff;
        name = statusName.Push;
    }
    public override Status copy()
    {
        return new Push(this, false);
    }
    public void setDir(Vector2 dir)
    {
        this.dir = dir;
    }

    public Push(PushSO pushSO) : base(pushSO.duration, pushSO.tick)
    {
        dir = pushSO.dir;
        maxSpeed = pushSO.maxSpeed;
        minSpeed = pushSO.minSpeed;
        pushFallOff = pushSO.pushFallOff;
        name = statusName.Push;
    }
    public Push(Vector2 dir, float maxSpeed, float minSpeed, float pushFallOff, float duration, float tick):base (duration, tick)
    {
        this.dir = dir;
        this.maxSpeed = maxSpeed;
        this.minSpeed = minSpeed;
        this.pushFallOff = pushFallOff;
        name = statusName.Push;

    }
    
    public override void resolvePhysicsEfects(HealthStatusManager HSman)
    {
        actualSpeed = this.dir.normalized * ((maxSpeed - minSpeed) * (1 - timer / duration) + minSpeed);
        HSman.GetComponent<Character2dTopDownControler>().enviromentSpeedVector.Add(actualSpeed);
    }
}