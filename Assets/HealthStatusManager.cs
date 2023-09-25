using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStatusManager : MonoBehaviour
{
    public float Hp;
    public float maxHp;
    public List<Status> Statuses = new List<Status>();
    public Sprite bleedIcon;
    // Start is called before the first frame update
    void Start()
    {
        Statuses.Add(new Bleed(1, 0.1f, 0.5f, bleedIcon));
        Statuses.Add(new Bleed(1, 0.1f, 0.5f, bleedIcon));
        Statuses.Add(new Bleed(1, 0.1f, 0.5f, bleedIcon));
        Statuses.Add(new Bleed(1, 0.1f, 0.5f, bleedIcon));
        Statuses.Add(new Bleed(1, 0.1f, 0.5f, bleedIcon));
        Statuses.Add(new Bleed(1, 0.1f, 0.5f, bleedIcon));
        Statuses.Add(new Bleed(1, 0.1f, 0.5f, bleedIcon));
        Statuses.Add(new Bleed(1, 0.1f, 0.5f, bleedIcon));
        Statuses.Add(new Bleed(1, 0.1f, 0.5f, bleedIcon));
        Statuses.Add(new Bleed(1, 0.1f, 0.5f, bleedIcon));
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = Statuses.Count -1 ; i >=0; i--)
        {
            if (Statuses[i].resolveStatus(Time.deltaTime, this))
                Statuses.RemoveAt(i);
        }  
    }

    public void takeDamage(float amount)
    {
        Hp -= amount;
        if (Hp <= 0)
            death();
    }

    void death()
    {

    }
}
