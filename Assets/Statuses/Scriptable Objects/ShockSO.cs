
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShockStatus", menuName = "ScriptableObjects/Statuses/Shock", order = 1)]
public class ShockSO : StatusSO
{
    public float damage;
    public float slow;
    private void Reset()
    {
        name = Status.statusName.Shock;
    }

    public override Status createObject()
    {
        Debug.Log("create Shock");
        return new Shock(this);
    }
}
