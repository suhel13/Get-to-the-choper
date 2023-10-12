using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    int statusId = 0;
    public static GameManager Instance { get; private set; }

    public IconManager iconManager { get; private set; }
    public UIManager uiManager { get; private set; }
    public BaseStatuses baseStatuses { get; private set; }
    public SpawnManager spawnManager { get; private set; }
    public PlayerUpgrades playerUpgrades { get; private set; }

    public GameObject player;

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
        iconManager = GetComponentInChildren<IconManager>();
        uiManager = GetComponentInChildren<UIManager>();
        baseStatuses = GetComponentInChildren<BaseStatuses>();
        spawnManager = GetComponentInChildren<SpawnManager>();
        playerUpgrades = GetComponentInChildren<PlayerUpgrades>();
    }
    public int nextStatusId()
    {
        return statusId++;
    }

    // Start is called before the first frame update
    void Start()
    {

    }
}