using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpParticle : MonoBehaviour, IPickAble
{
    public int xpAmount;
    public void pickUp()
    {

        GameManager.Instance.playerUpgrades.addXp(xpAmount);
        Destroy(gameObject);
    }
}
