using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WepponManager : MonoBehaviour
{
    public List<Weppon> WepponList;
    public Weppon activeWeppon;
    public bool isAttacking;
    PersonalUIControler personalUIControler;
    [SerializeField] bool isPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        personalUIControler = GetComponentInChildren<PersonalUIControler>();
        WepponList = GetComponentsInChildren<Weppon>(true).ToList<Weppon>();
        activeWeppon = WepponList[0];
        Debug.Log(WepponList.Count);
        Debug.Log(WepponList[0]);
        Debug.Log(GameManager.Instance.uiManager.wepponIconControlers[0]);

        for (int i = 0; i < WepponList.Count; i++)
        {
            Debug.Log(i);
            if (WepponList[i] is Gun)
            {
                (WepponList[i] as Gun).relodeSlider = personalUIControler.relodeSlider;
            }
            Debug.Log(GameManager.Instance.uiManager.wepponIconControlers[i]);

            if (isPlayer)
            {
                WepponList[i].iconControler = GameManager.Instance.uiManager.wepponIconControlers[i];
            }
        } 
        if(isPlayer)
        {
            WepponList[0].iconControler.ActiveWepponIcon.SetActive(true);
        }

        personalUIControler.relodeSlider.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isAttacking)
        {
            attack();
        }
        else
        {
            activeWeppon.stopedShooting = true;
        }
        updateGunsTimers(Time.deltaTime);
    }
    public void attack()
    {
        activeWeppon.attack();
    }
    public void swapWeppon(int id)
    {
        activeWeppon.iconControler.ActiveWepponIcon.SetActive(false);
        activeWeppon.cancelRelode();
        if(WepponList.Count > id) 
        {
            activeWeppon = WepponList[id];
            activeWeppon.iconControler.ActiveWepponIcon.SetActive(true);

            if((activeWeppon as Gun).mag == 0)
            {
                activeWeppon.startRelode();
            }
        }
    }

    public void updateGunsTimers(float delthaTime)
    {
        foreach (Weppon weppon in WepponList)
        {
            weppon.updateGunsTimers(delthaTime);
        }
        if(activeWeppon is Gun)
            activeWeppon.updateGunRelodeTimer(delthaTime);
    }
}