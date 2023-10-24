using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NumberControler : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI damageAmount;
    [SerializeField] Image damageTypeIcon;
    public void Set(float amount)
    {
        damageAmount.text = amount.ToString();
    }
}