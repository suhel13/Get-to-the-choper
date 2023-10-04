using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAcidStatus", menuName = "ScriptableObjects/Statuses/Acid", order = 1)]
public class AcidSO : StatusSO
{
    public float damage;
    public float armorReduction;
    private void Reset()
    {
        name = Status.statusName.Acid;
    }
    public override Status createObject()
    {
        Debug.Log("create Acid");
        return new Acid(this);
    }
}
