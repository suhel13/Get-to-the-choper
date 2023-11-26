using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public enum gameState { play, pause, upgradePause}
    public gameState state;
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
        float testflaot = 1241;
        string testString;
        var watch = new System.Diagnostics.Stopwatch();
        watch.Start();
        for (int i = 0; i < 1000; i++)
        {
            testString = testflaot.ToString();
        }
        watch.Stop();
        Debug.Log($"Execution Time float.ToString(): {watch.ElapsedMilliseconds} ms");

        var watch2 = new System.Diagnostics.Stopwatch();
        watch2.Start();
        for (int i = 0; i < 1000; i++)
        {
            testString = "" + testflaot;
        }
        watch2.Stop();
        Debug.Log($"Execution Time : \"\"+ float {watch2.ElapsedMilliseconds} ms");

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
    public void changeState(gameState state)
    {
        switch (state)
        {
            case gameState.play:
                Time.timeScale = 1;
                break;
            case gameState.pause:
                Time.timeScale = 0;
                break;
            case gameState.upgradePause:
                Time.timeScale = 0;
                break;
        }
        this.state = state;
    }

    // Start is called before the first frame update
    void Start()
    {

    }
}