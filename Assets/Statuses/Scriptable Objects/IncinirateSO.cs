using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIncinirateStatus", menuName = "ScriptableObjects/Statuses/Incinirate", order = 1)]
public class IncinirateSO : StatusSO
{
    public float damage;
    public float bleedDamage;
    private void Reset()
    {
        name = Status.statusName.Insinirate;
    }

    public override Status createObject()
    {
        Debug.Log("create Incinirate");
        return new Incinirate(this);
    }
}