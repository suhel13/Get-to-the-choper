using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBleedStatus", menuName = "ScriptableObjects/Statuses/Bleed", order = 1)]
public class BleedSO : StatusSO
{
    public float damage;

    public override Status createObject()
    {
        Debug.Log("create Bleed");
        return new Bleed(this);
    }
}
