using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWetStatus", menuName = "ScriptableObjects/Statuses/Wet", order = 1)]
public class WetSO : StatusSO
{
    private void Reset()
    {
        name = Status.statusName.Wet;
    }
    public override Status createObject()
    {
        return new Wet(this);
    }
}