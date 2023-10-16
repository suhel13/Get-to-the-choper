using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.WSA;

public class SpawnManager : MonoBehaviour
{
    public enum enemyType { knife, pistol }
    public GameObject xpParticlePrefab;
    public GameObject knifeEnemyPrefab;
    public GameObject pistolEnemyPrefab;

    List<GameObject> enemys = new List<GameObject>();
    public float innerSpawnRadius;
    public float outerSpawnRadius;
    int spawnLevel = 0;
    public List<float> spawnsLevelTime = new List<float>();
    float levelTimer;
    public List<float> spawnsTime = new List<float>();
    float spawsTimer;

    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {   
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.forward, innerSpawnRadius);
        Handles.DrawWireDisc(transform.position, Vector3.forward, outerSpawnRadius);
        Handles.DrawWireDisc(transform.position, Vector3.forward, (innerSpawnRadius + outerSpawnRadius) /2 );
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        levelTimer += Time.deltaTime;
        spawsTimer += Time.deltaTime;
        if (levelTimer >= spawnsLevelTime[spawnLevel])
        {
            levelTimer = 0;
            spawnLevel++;
        }

        if (spawsTimer >= spawnsTime[spawnLevel])
        {
            spawsTimer = 0;

            switch (spawnLevel)
            {
                case 0:
                    spawEnemy(enemyType.knife);
                    break;
                case 1:
                    spawEnemy(enemyType.knife);
                    break;
            }
        }
    }

    Vector2 getSpawnPos()
    {
        float radius = Random.Range(innerSpawnRadius, outerSpawnRadius);
        float angle = Random.Range(0f, 2*Mathf.PI);
        return new Vector2(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle));
    }
    void spawEnemy(enemyType type)
    {
        switch(type) 
        {
            case enemyType.knife:
                enemys.Add(Instantiate(knifeEnemyPrefab, getSpawnPos(), Quaternion.identity));
                break;
            case enemyType.pistol:
                enemys.Add(Instantiate(knifeEnemyPrefab, getSpawnPos(), Quaternion.identity));
                break;
        }
    }
    public void spawnXp(int amount, Vector3 position)
    {
        Instantiate(xpParticlePrefab, position, Quaternion.identity).GetComponent<XpParticle>().xpAmount = amount;
    }
}