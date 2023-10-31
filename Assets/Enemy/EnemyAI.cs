using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Character2dTopDownControler))]
[RequireComponent(typeof(WepponManager))]
public class EnemyAI : MonoBehaviour
{
    Character2dTopDownControler _characterControler;
    WepponManager _wepponManager;
    Camera mainCam;

    public float attackRange;
    public EnemyType type;
    public enum EnemyType { mele, range }
    void Start()
    {
        mainCam = Camera.main;

        _characterControler = GetComponent<Character2dTopDownControler>();
        _wepponManager = GetComponent<WepponManager>();
    }
    private void Update()
    {
        lookAt(GameManager.Instance.player.transform.position);
        if (Vector2.Distance(GameManager.Instance.player.transform.position, transform.position) > attackRange)
        {
            switch (type)
            {
                case EnemyType.mele:
                    setMovementVector((GameManager.Instance.player.transform.position - transform.position).normalized);
                    break;

                case EnemyType.range:

                    break;

                default: break;
            }
        }
        else
        {
            setMovementVector(Vector2.zero);
            switch (type)
            {
                case EnemyType.mele:
                    _wepponManager.activeWeppon.Attack();
                    break;

                case EnemyType.range:

                    break;

                default: break;
            }
        }
    }

    void setMovementVector(Vector2 dir)
    {
        _characterControler.movementVector = dir;
    }
    void lookAt(Vector2 pos)
    {
        _characterControler.lookAtTarget.position = pos;
    }
}