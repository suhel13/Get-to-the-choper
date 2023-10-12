using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPushStatus", menuName = "ScriptableObjects/Statuses/Push", order = 1)]
public class PushSO : StatusSO
{
    public Vector2 dir;
    public float maxSpeed;
    public float minSpeed;
    public float pushFallOff;
    private void Reset()
    {
        name = Status.statusName.Push;
    }
    public override Status createObject()
    {
        return new Push(this);
    }
}