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
    public Transform lookAtTarget;
    public TMPro.TextMeshProUGUI ammoCounterText;
    public List<PasiveWeppon> pasiveWepponList;

    // Start is called before the first frame update
    void Start()
    {
        personalUIControler = GetComponentInChildren<PersonalUIControler>();
        ammoCounterText = lookAtTarget.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        WepponList = GetComponentsInChildren<Weppon>(true).ToList<Weppon>();
        activeWeppon = WepponList[0];
        Debug.Log(WepponList.Count);
        Debug.Log(WepponList[0]);
        Debug.Log(GameManager.Instance.uiManager.wepponIconControlers[0]);
        for (int i = WepponList.Count - 1; i >= 0; i--)
        {
            if (WepponList[i] is PasiveWeppon)
            {
                WepponList.RemoveAt(i);
            }
        }
        for (int i = 0; i < WepponList.Count; i++)
        {
            Debug.Log(i);
            if (WepponList[i] is Gun)
            {
                (WepponList[i] as Gun).relodeSlider = personalUIControler.relodeSlider;
                if (ammoCounterText != null)
                {
                    (WepponList[i] as Gun).ammoCounterText = ammoCounterText;
                }
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
            Attack();
        }
        else
        {
            activeWeppon.StartRelode(false);
        }
        UpdateGunsTimers(Time.deltaTime);
        foreach (PasiveWeppon pasiveWeppon in pasiveWepponList)
        {
            pasiveWeppon.Attack();
            pasiveWeppon.UpdateGunsTimers(Time.deltaTime);
        }
    }
    public void Attack()
    {
        activeWeppon.Attack(lookAtTarget.position);
    }
    public void SwapWeppon(int id)
    {
        activeWeppon.iconControler.ActiveWepponIcon.SetActive(false);
        activeWeppon.CancelRelode();
        if(WepponList.Count > id) 
        {
            activeWeppon = WepponList[id];
            activeWeppon.iconControler.ActiveWepponIcon.SetActive(true);

            if(activeWeppon is Gun)
            {
                activeWeppon.StartRelode(false);
                (activeWeppon as Gun).UpdateAmmoCounterText();
            }
        }
    }

    public void UpdateGunsTimers(float delthaTime)
    {
        foreach (Weppon weppon in WepponList)
        {
            weppon.UpdateGunsTimers(delthaTime);
        }
        if(activeWeppon is Gun)
            activeWeppon.UpdateGunRelodeTimer(delthaTime);
    }
}