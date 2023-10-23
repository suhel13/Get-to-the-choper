using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpParticle : MonoBehaviour, IPickAble
{
    public int xpAmount;
    public void pickUp()
    {

        GameManager.Instance.playerUpgrades.AddXp(xpAmount);
        Destroy(gameObject);
    }
}
