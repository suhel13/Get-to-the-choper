using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasiveWeppon : Weppon
{
    public void Start()
    {
        base.Start();
        GetComponentInParent<WepponManager>().pasiveWepponList.Add(this);
    }
}
