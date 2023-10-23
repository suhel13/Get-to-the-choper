using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PasiveWeppon : Weppon
{
    public void Start()
    {
        base.Start();
        GetComponentInParent<WepponManager>().pasiveWepponList.Add(this);
    }
    public override void attack()
    {
        if (fireRateTimer <= 0)
        {
            //.SetTrigger("Attack");
            fireRateTimer = 1 / baseFireRate;
            Effect();
        }
    }
    public abstract void Effect();
}
