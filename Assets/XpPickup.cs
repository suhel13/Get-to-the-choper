using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class XpPickup : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.playerUpgrades.PickUpRangeUpgraded += PlayerUpgrades_PickUpRangeUpgraded;
        basePickUpRange = 1;
        pickUpRange = basePickUpRange * (1 + GameManager.Instance.playerUpgrades.pickUpRangeBonus);
    }

    private void PlayerUpgrades_PickUpRangeUpgraded() { pickUpRange = basePickUpRange * (1 + GameManager.Instance.playerUpgrades.pickUpRangeBonus); }

    float pickUpRange;
    [SerializeField] float basePickUpRange;
   
    IPickAble temp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<IPickAble>(out temp))
        {
            temp.StartPickUp();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IPickAble>(out temp))
        {
            temp.PickUp();
        }
    }
}