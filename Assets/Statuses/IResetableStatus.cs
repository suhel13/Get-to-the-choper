using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResetableStatus <T>
{
    public void resetStatus(T newStatus);
}
