using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PasiveWeppon : Weppon
{
    protected new void Start()
    {
        base.Start();
        GetComponentInParent<WepponManager>().pasiveWepponList.Add(this);
    }
    public override void Attack()
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
