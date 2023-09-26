using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusSO : ScriptableObject
{
    public Status.statusName name; 
    public float duration;
    public float tick;
    public Sprite icon;

    public virtual Status createObject()
    {
        Debug.Log("create Status");
        return null;
    }
}
