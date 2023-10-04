using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class EnviomentStatusEfect : MonoBehaviour
{
    [SerializeField] List<StatusSO> statusesSO = new List<StatusSO>();
    List<Status> statuses = new List<Status>();
    public float duration;
    float timer;
    public bool isInfinite;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var statSO in statusesSO)
        {
            Debug.Log(statSO.name);
            statuses.Add(statSO.createObject());
        }
        timer = 0.0f;
    }
    private void Update()
    {
        if(isInfinite) 
            return;
        timer += Time.deltaTime;
        if(timer > duration)
            Destroy(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        HealthStatusManager target;
        if (collision.gameObject.TryGetComponent<HealthStatusManager>(out target))
        {
            foreach (Status status in statuses)
            {
                target.addStatus(status.copy());
            }
        }
    }
}