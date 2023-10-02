using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class EnviomentStatusEfect : MonoBehaviour
{
    [SerializeField] List<StatusSO> statusesSO = new List<StatusSO>();
    List<Status> statuses = new List<Status>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var statSO in statusesSO)
        {
            Debug.Log(statSO.name);
            statuses.Add(statSO.createObject());
        }
        Debug.Log(statuses.Count, this.gameObject);
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