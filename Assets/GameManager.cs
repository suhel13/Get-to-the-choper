using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    int statusId = 0;
    public static GameManager Instance { get; private set; }

    public IconManager iconManager { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        Debug.Log(nextStatusId());
        Debug.Log(nextStatusId());
    }
    public int nextStatusId()
    {
        return statusId++;
    }

    // Start is called before the first frame update
    void Start()
    {
        iconManager = GetComponentInChildren<IconManager>();
    }
}