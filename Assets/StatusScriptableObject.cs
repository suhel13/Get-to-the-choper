using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStatus", menuName = "ScriptableObjects/Status", order = 1)]
public class StatusScriptableObject : ScriptableObject
{
    public Status.statusName name; 
    public float duration;
    public float tick;
    public Sprite icon;
}
