using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WepponManager : MonoBehaviour
{
    public List<Gun> GunList;
    public Gun activeGun;
    public bool isShooting;
    PersonalUIControler personalUIControler;

    // Start is called before the first frame update
    void Start()
    {
        personalUIControler = GetComponentInChildren<PersonalUIControler>();
        GunList = GetComponentsInChildren<Gun>(true).ToList<Gun>();
        activeGun = GunList[0];
        foreach (Gun gun in GunList)
        {
            gun.relodeSlider = personalUIControler.relodeSlider;
        }
        personalUIControler.relodeSlider.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isShooting)
        {
            shoot();
        }
        else
        {
            activeGun.stopedShooting = true;
        }
        updateGunsTimers(Time.deltaTime);
    }
    public void shoot()
    {
        activeGun.shoot();
    }
    public void nextGun()
    {

    }

    public void updateGunsTimers(float delthaTime)
    {
        foreach (Gun gun in GunList)
        {
            gun.updateGunsTimers(delthaTime);
        }
        activeGun.updateGunsRelodeTimers(delthaTime);
    }
}
