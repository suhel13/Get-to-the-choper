using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSmokeStatus", menuName = "ScriptableObjects/Statuses/Smoke", order = 1)]
public class SmokeSO : StatusSO
{
    public float acuracityLoss;

    private void Reset()
    {
        name = Status.statusName.Smoke;
    }

    public override Status createObject()
    {
        Debug.Log("create Smoke");
        return new Smoke(this);
    }
}