using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleWeppno : Weppon
{
    HealthStatusManager HSman;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger ", collision.gameObject);
        if(collision.TryGetComponent(out  HSman))
        {
            Debug.Log("mele damage triger");
            HSman.TakeDamage(baseDamage);
        }
    }
}