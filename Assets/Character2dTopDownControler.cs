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
    public Dictionary<int, float> speedModifires = new Dictionary<int, float>();
    public List<Vector2> enviromentSpeedVector = new List<Vector2>();
    public Vector2 movementVector;
    public Transform lookAtTarget;

    public WepponManager wepponManager;
    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        lookAtTarget.transform.parent = transform.parent;
        wepponManager = GetComponent<WepponManager>();

    }
    private void Update()
    {
        float rotation = Mathf.Atan2((lookAtTarget.position.x - this.transform.position.x), (lookAtTarget.position.y - this.transform.position.y)) * 180 / Mathf.PI * -1;
        this.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotation);
    }

    public void addSpeedModifire(int id, float modifire)
    {
        if(speedModifires.ContainsKey(id) == false) 
        {
            speedModifires.Add(id, modifire);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speedModifire = 1;
        foreach (KeyValuePair<int, float> entry in speedModifires)
        {
            speedModifire += entry.Value;
        }
        speedModifires.Clear();

        rb2D.velocity = movementVector * speed * speedModifire;
        
        foreach(Vector2 evnSpeed in enviromentSpeedVector) 
        {
            rb2D.velocity += evnSpeed;
        }
        enviromentSpeedVector.Clear();

    }
}
