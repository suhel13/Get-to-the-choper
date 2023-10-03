using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WepponIconControler : MonoBehaviour
{
    [SerializeField] Image wepponIcon;
    [SerializeField] TMPro.TextMeshProUGUI ammoText;
    public GameObject ActiveWepponIcon;
    public void updateWepponIconAmmo(string text)
    {
        ammoText.text = text;
    }
}