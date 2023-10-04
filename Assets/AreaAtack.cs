using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAtack : MonoBehaviour
{
    static HealthStatusManager tempHSMan;
    public static void resolveAreaAtack(float radius, float damage, Push push, Vector3 center, bool pushFallOff)
    {
        foreach (var item in Physics2D.OverlapCircleAll(center, radius))
        {
            if(item.TryGetComponent<HealthStatusManager>(out tempHSMan))
            {
                tempHSMan.takeDamage(damage);
                if(pushFallOff)
                {
                    tempHSMan.addPush(push, (tempHSMan.transform.position - center).normalized * Vector3.Distance(tempHSMan.transform.position, center)/radius * 0.5f);
                }
                else
                    tempHSMan.addPush(push, (tempHSMan.transform.position - center).normalized);
            }
        }
        
    }
}
