using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviormentMovementEffect : MonoBehaviour
{
    public Vector2 diretion;
    public float speed;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Character2dTopDownControler target;
        if (collision.gameObject.TryGetComponent<Character2dTopDownControler>(out target))
        {
            target.enviromentSpeedVector.Add(diretion * speed);
        }
    }

}
