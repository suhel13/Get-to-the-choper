using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviomentStatusEfect : MonoBehaviour
{
    [SerializeField] List<StatusSO> statusesSO = new List<StatusSO>();
    List<Status> statuses;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var statSO in statusesSO)
        {
            Debug.Log(statSO.name);
            statuses.Add(statSO.createObject());
        }
    }

}
