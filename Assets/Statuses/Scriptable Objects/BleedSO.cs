using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBleedStatus", menuName = "ScriptableObjects/Statuses/Bleed", order = 1)]
public class BleedSO : StatusSO
{
    public float damage;

    private void Reset()
    {
        name = Status.statusName.Bleed;
    }
    public override Status createObject()
    {
        Debug.Log("create Bleed");
        return new Bleed(this);
    }
}
