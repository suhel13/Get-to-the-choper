using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFrezeStatus", menuName = "ScriptableObjects/Statuses/Freze", order = 1)]
public class FreezeSO : StatusSO
{

    public float slow;
    private void Reset()
    {
        name = Status.statusName.Freeze;
    }
    public override Status createObject()
    {
        Debug.Log("create Freze");
        return new Freeze(this);
    }
}
