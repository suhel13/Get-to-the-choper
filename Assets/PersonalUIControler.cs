using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonalUIControler : MonoBehaviour
{
    public List<StatusIconControler> statusIcons = new List<StatusIconControler>();
    public GameObject statusIconPrefab;

    public Transform firstStatusIcon;
    public Vector3 statusIconOffset;
    public Slider hpSlider;
    public Slider relodeSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateStatusIconPositions()
    {
        for (int i = 0; i < statusIcons.Count; i++)
        {
            statusIcons[i].setPosition(firstStatusIcon.GetComponent<RectTransform>().localPosition + i * statusIconOffset);
        }
    }
    public void updateHpSlider(float hp)
    {
        hpSlider.value = hp;
    }
}