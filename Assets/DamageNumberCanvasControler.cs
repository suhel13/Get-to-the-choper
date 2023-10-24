using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberCanvasControler : MonoBehaviour
{
    [SerializeField] GameObject numberPrefab;
    [SerializeField] Transform topLeft;
    [SerializeField] Transform bottomRight;
    NumberControler tempNumberControler;

    public void SpawnDamageNumber(float amount)
    {
        Vector2 spawnPos;
        spawnPos.x = Random.Range(topLeft.position.x, bottomRight.position.x);
        spawnPos.y = Random.Range(topLeft.position.y, bottomRight.position.y);
        tempNumberControler = Instantiate(numberPrefab, spawnPos, Quaternion.identity, transform).GetComponent<NumberControler>();
        tempNumberControler.Set(amount);
    }
}