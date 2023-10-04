using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewColdStatus", menuName = "ScriptableObjects/Statuses/Cold", order = 1)]
public class ColdSO : StatusSO
{

    public float slow;
    [Header("Combo Efect")]

    private void Reset()
    {
        name = Status.statusName.Cold;
    }
    public override Status createObject()
    {
        Debug.Log("create Freze");
        return new Cold(this);
    }
}
