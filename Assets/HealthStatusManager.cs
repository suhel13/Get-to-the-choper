using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.EventSystems.EventTrigger;

public class HealthStatusManager : MonoBehaviour, IPushAble, IDamageAble
{
    public bool isPlayer;
    public float Hp;
    [SerializeField] float baseMaxHp;
    float maxHp;
    
    public Dictionary<int, Status> Statuses = new Dictionary<int, Status>();
    List<int> statusesToRemove = new List<int>();

    public List<Push> pushes = new List<Push>();
    List<int> pushesToRemove = new List<int>();

    PersonalUIControler personalUIControler;
    public int xpAmount;
    DamageNumberCanvasControler damageNumberCanvasControler;

    void PlayerUpgrades_HpUpgraded() { maxHp = baseMaxHp * (1 + GameManager.Instance.playerUpgrades.statusDurationBonus); }
    // Start is called before the first frame update
    void Start()
    {
        damageNumberCanvasControler = GetComponentInChildren<DamageNumberCanvasControler>();
        if (isPlayer)
        {
            GameManager.Instance.playerUpgrades.HpUpgraded += PlayerUpgrades_HpUpgraded;
            maxHp = baseMaxHp * (1 + GameManager.Instance.playerUpgrades.statusDurationBonus);
            Hp = maxHp;
        }
        else
        {
            maxHp = baseMaxHp;
            Hp = maxHp;
        }
        personalUIControler = GetComponentInChildren<PersonalUIControler>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyValuePair<int, Status> entry in Statuses)
        {
            entry.Value.startEfect(this);

            // do something with entry.Value or entry.Key
            if (entry.Value.resolveStatus(Time.deltaTime, this))
            {
                statusesToRemove.Add(entry.Key);
            }
            else
            {
                Statuses[entry.Key].statusIconUpdate();
            }
        }
        foreach (int key in statusesToRemove)
        {
            RemoveStatus(key);
        }
        statusesToRemove.Clear();

        foreach (Push push in pushes)
        {
            push.startEfect(this);
            if (push.resolveStatus(Time.deltaTime, this))
            {
                pushesToRemove.Add(pushes.IndexOf(push));
            }
        }
        foreach (int key in pushesToRemove)
        {
            RemovePush(key);
        }
        pushesToRemove.Clear();

        personalUIControler.updateHpSlider(Hp / maxHp);
    }

    private void FixedUpdate()
    {
        foreach (KeyValuePair<int, Status> entry in Statuses)
        {
            entry.Value.resolvePhysicsEfects(this);
        }
        foreach (Push push in pushes)
        {
            push.resolvePhysicsEfects(this);
        }
    }


    public bool TakeDamage(float amount)
    {
        Hp -= amount;

        if(damageNumberCanvasControler)
            damageNumberCanvasControler.SpawnDamageNumber(amount);

        if (Hp <= 0)
        {
            death();
            return false;
        }
        else return true;
    }
    public void RemoveStatus(int id)
    {

            Destroy(Statuses[id].statusIcon.gameObject);
            personalUIControler.statusIcons.Remove(Statuses[id].statusIcon);
            personalUIControler.updateStatusIconPositions();
    }
    public void RemovePush(int id)
    {
        pushes.RemoveAt(id);
    }

    public void addStatus(Status status)
    {
        if (status is Push)
            return;
        if (status.resolveCombinations(this, Statuses))
        {
            if (Statuses.ContainsKey(status.id))
            {
                Statuses[status.id].resetStatus(status);
            }
            else
            {
                Statuses.Add(status.id, status);
                StatusIconControler statusIconControler = personalUIControler.createStatusIcon(status.name);
                personalUIControler.statusIcons.Add(statusIconControler);
                status.statusIcon = statusIconControler;
            }
        }
    }

    public void addPush(Push push, Vector2 dir)
    {
        push.resolveCombinations(this, Statuses);
        push.setDir(dir);
        pushes.Add(push);
    }
    bool HasStatus(Status.statusName statusName)
    {
        foreach (KeyValuePair<int, Status> entry in Statuses)
        {
            if (entry.Value.name == statusName)
                return true;
        }
        return false;
    }
    bool HasStatus(Status.statusName statusName, out int key)
    {
        foreach (KeyValuePair<int, Status> entry in Statuses)
        {
            if (entry.Value.name == statusName)
            {
                key = entry.Key;
                return true;
            }
        }
        key = -1;
        return false;
    }

    void death()
    {
        if (xpAmount > 0)
        {
            GameManager.Instance.spawnManager.spawnXp(xpAmount, transform.position);
        }
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.Instance.baseStatuses.pushCollision.value == (GameManager.Instance.baseStatuses.pushCollision.value|( 1 << collision.gameObject.layer) ) )
        { 
            Vector2 pushForce = Vector2.zero;
            foreach (Push push in pushes)
            {
                pushForce += push.actualSpeed;
            }

            Debug.Log(pushForce, gameObject);
            if (pushForce.magnitude < 0.2f)
                return;

            float collisionDamageMultiplayer = 1;

            if (HasStatus(Status.statusName.Frozen))
                collisionDamageMultiplayer *= 2;

            int tempShockKey;
            HealthStatusManager collisionTargetHSMan;
            
            if (HasStatus(Status.statusName.Shock, out tempShockKey))
                if (collision.gameObject.TryGetComponent(out collisionTargetHSMan))
                    collisionTargetHSMan.addStatus(Statuses[tempShockKey]);

            TakeDamage(pushForce.magnitude * collisionDamageMultiplayer);
            Debug.Log("Collission damage: " + (pushForce.magnitude * collisionDamageMultiplayer).ToString(), gameObject);
        }
    }
}