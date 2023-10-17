using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class XpPickup : MonoBehaviour
{
    private void Start()
    {
        pickUpRange = 1;
    }
    [SerializeField] public float pickUpRange
    {
        get { return pickUpRange; }
        set
        {
            GetComponent<CircleCollider2D>().radius = value;
        }
    }

    IPickAble temp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<IPickAble>(out temp))
        {
            temp.pickUp();
        }
    }
}