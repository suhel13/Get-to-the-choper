using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStatusManager : MonoBehaviour, IPushAble, IDamageAble
{
    public bool isPlayer;
    public float Hp;
    [SerializeField] float baseMaxHp;
    float maxHp;
    
    public Dictionary<int, Status> Statuses = new Dictionary<int, Status>();
    List<int> statusesToRemove = new List<int>();
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
                if (entry.Value is Push == false)
                    Statuses[entry.Key].statusIconUpdate();
            }
        }
        foreach (int key in statusesToRemove)
        {
            removeStatus(key);
        }

        statusesToRemove.Clear();

        personalUIControler.updateHpSlider(Hp / maxHp);
    }

    private void FixedUpdate()
    {
        foreach (KeyValuePair<int, Status> entry in Statuses)
        {
            entry.Value.resolvePhysicsEfects(this);
        }
    }


    public bool takeDamage(float amount)
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
    public void removeStatus(int id)
    {
        if (Statuses[id] is Push == false)
        {
            Destroy(Statuses[id].statusIcon.gameObject);
            personalUIControler.statusIcons.Remove(Statuses[id].statusIcon);
            personalUIControler.updateStatusIconPositions();
        }
        Statuses.Remove(id);
    }

    public void addStatus(Status status)
    {
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
        if (Statuses.ContainsKey(push.id))
        {
            Statuses[push.id].resetStatus(push);
        }
        else
        {
            Statuses.Add(push.id, push);
        }
    }

    void death()
    {
        if (xpAmount > 0)
        {
            GameManager.Instance.spawnManager.spawnXp(xpAmount, transform.position);
        }
        Destroy(this.gameObject);
    }
}