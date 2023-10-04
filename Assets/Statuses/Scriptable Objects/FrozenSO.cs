using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFrozenStatus", menuName = "ScriptableObjects/Statuses/Frozen", order = 1)]
public class FrozenSO : StatusSO
{
    private void Reset()
    {
        name = Status.statusName.Frozen;
    }
    public override Status createObject()
    {
        Debug.Log("create Freze");
        return new Frozen(this);
    }
}