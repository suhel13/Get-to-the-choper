using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpParticle : MonoBehaviour, IPickAble
{
    float pickupSpeed;
    Rigidbody2D rb2D;
    public int xpAmount;
    private void Start()
    {
        pickupSpeed = GameManager.Instance.player.GetComponent<Character2dTopDownControler>().speed * 1.5f;
        rb2D = GetComponent<Rigidbody2D>();
    }
    public void PickUp()
    {
        GameManager.Instance.playerUpgrades.AddXp(xpAmount);
        StopCoroutine(IStartPickUp());
        Destroy(gameObject);
    }
    public void StartPickUp()
    {
        StartCoroutine(IStartPickUp());
    }
    IEnumerator IStartPickUp()
    {
        while (true)
        {
            rb2D.velocity = (GameManager.Instance.player.transform.position - transform.position).normalized * pickupSpeed;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
