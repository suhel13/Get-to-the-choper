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
    [SerializeField] bool isPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        personalUIControler = GetComponentInChildren<PersonalUIControler>();
        GunList = GetComponentsInChildren<Gun>(true).ToList<Gun>();
        activeGun = GunList[0];
        Debug.Log(GunList.Count);
        Debug.Log(GunList[0]);
        Debug.Log(GameManager.Instance.uiManager.wepponIconControlers[0]);

        for (int i = 0; i < GunList.Count; i++)
        {
            Debug.Log(i);
            GunList[i].relodeSlider = personalUIControler.relodeSlider;
            Debug.Log(GameManager.Instance.uiManager.wepponIconControlers[i]);

            if (isPlayer)
            {
                GunList[i].iconControler = GameManager.Instance.uiManager.wepponIconControlers[i];
            }
        } 
        if(isPlayer)
        {
            GunList[0].iconControler.ActiveWepponIcon.SetActive(true);
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
    public void swapWeppon(int id)
    {
        activeGun.iconControler.ActiveWepponIcon.SetActive(false);
        activeGun.cancelRelode();
        if(GunList.Count > id) 
        {
            activeGun = GunList[id];
            activeGun.iconControler.ActiveWepponIcon.SetActive(true);
            if(activeGun.mag == 0)
            {
                activeGun.startRelode();
            }
        }
    }

    public void updateGunsTimers(float delthaTime)
    {
        foreach (Gun gun in GunList)
        {
            gun.updateGunsTimers(delthaTime);
        }
        activeGun.updateGunRelodeTimer(delthaTime);
    }
}
