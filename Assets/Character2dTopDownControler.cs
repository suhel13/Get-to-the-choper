using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Character2dTopDownControler : MonoBehaviour
{
    public bool isPlayer;
    public bool isAlive;
    public bool canMove;
    [SerializeField] float baseSpeed;
    [HideInInspector] public float speed;
    public bool canRotate;
    [SerializeField] float rotateSpeed;
    public float speedModifire = 1;
    public Dictionary<int, float> speedModifires = new Dictionary<int, float>();
    public List<Vector2> enviromentSpeedVector = new List<Vector2>();
    public Vector2 movementVector;
    public Transform lookAtTarget;
    public TMPro.TextMeshProUGUI ammoCounterText;
    public WepponManager wepponManager;
    Rigidbody2D rb2D;

    PersonalUIControler persControler;

    private void PlayerUpgrades_MovementSpeedUpgraded() { speed = baseSpeed * (1 + GameManager.Instance.playerUpgrades.movementSpeedBonus); }

    // Start is called before the first frame update
    void Start()
    {
        if (isPlayer)
        {
            GameManager.Instance.playerUpgrades.MovementSpeedUpgraded += PlayerUpgrades_MovementSpeedUpgraded;
            speed = baseSpeed * (1 + GameManager.Instance.playerUpgrades.movementSpeedBonus);
            ammoCounterText = lookAtTarget.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        }
        else
            speed = baseSpeed;

        rb2D = GetComponent<Rigidbody2D>();
        lookAtTarget.transform.parent = transform.parent;
        wepponManager = GetComponent<WepponManager>();
        persControler = GetComponentInChildren<PersonalUIControler>();
        GetComponent<WepponManager>().lookAtTarget = lookAtTarget;
    }

    private void Update()
    {
        if (canRotate)
            rotate();
        else
            canRotate = true;
    }
    void rotate()
    {
        float angleDiff = Mathf.Atan2((lookAtTarget.position.x - this.transform.position.x), (lookAtTarget.position.y - this.transform.position.y)) * 180 / Mathf.PI * (-1) - transform.eulerAngles.z;
        if (angleDiff > 180)
            angleDiff -= 360;
        else if (angleDiff < -180)
            angleDiff += 360;

        if (angleDiff > 0)
        {
            if (rotateSpeed * Time.deltaTime < angleDiff)
            {
                transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
            }
                //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + rotateSpeed * Time.deltaTime);
            else
            {
                transform.Rotate(Vector3.forward, angleDiff);
            }
                //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + angleDiff);
        }
        else if (angleDiff < 0)
        {
            if (rotateSpeed * Time.deltaTime < Mathf.Abs(angleDiff))
            {

                transform.Rotate(Vector3.forward, - rotateSpeed * Time.deltaTime);
            }
                //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + rotateSpeed * Time.deltaTime);
            else
            {
                transform.Rotate(Vector3.forward, angleDiff);
            }
                //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + angleDiff);
        }
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
        if (canMove)
        {
            speedModifire = 1;
            foreach (KeyValuePair<int, float> entry in speedModifires)
            {
                speedModifire += entry.Value;
            }
            speedModifires.Clear();
            rb2D.velocity = movementVector * speed * speedModifire;
        }
        else
        {
            rb2D.velocity = Vector2.zero;
        }
        canMove = true;
        Debug.Log("base velocity: " + rb2D.velocity, gameObject);
        Debug.Log("push effects: " + enviromentSpeedVector.Count);

        foreach(Vector2 evnSpeed in enviromentSpeedVector) 
        {
            rb2D.velocity += evnSpeed;
        }

        enviromentSpeedVector.Clear();
        Debug.Log(rb2D.velocity, gameObject);
    }
}