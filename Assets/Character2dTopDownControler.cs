using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Character2dTopDownControler : MonoBehaviour
{
    [SerializeField] float speed;
    public float speedModifire = 1;
    public Vector2 enviromentSpeedVector;
    public Vector2 movementVector;

    public Transform lookAtTarget;

    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        lookAtTarget.transform.parent = transform.parent;

    }
    private void Update()
    {
        float rotation = Mathf.Atan2((lookAtTarget.position.x - this.transform.position.x), (lookAtTarget.position.y - this.transform.position.y)) * 180 / Mathf.PI * -1;
        this.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotation);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2D.velocity = movementVector * speed * speedModifire + enviromentSpeedVector;    
    }
}
