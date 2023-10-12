using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : Status
{
    Vector2 dir;
    float maxSpeed;
    float minSpeed;
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
    
    public override void resolvePhysicsEfects(HealthStatusManager HSman)
    {
        HSman.GetComponent<Character2dTopDownControler>().enviromentSpeedVector.Add(this.dir.normalized * ((maxSpeed - minSpeed) * (1 - timer / duration) + minSpeed));
    }
}