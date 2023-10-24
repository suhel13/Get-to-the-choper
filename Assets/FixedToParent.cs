using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedToParent : MonoBehaviour
{
    Transform parent;
    Vector3 posOffSet;
    Quaternion startRotation;
    private void Awake()
    {
        parent = transform.parent.transform;
        startRotation = transform.rotation;
        posOffSet = transform.localPosition;
    }

    // Update is called once per frame
    void updatePosAndRot()
    {
        transform.position = parent.position + posOffSet;
        transform.rotation = startRotation;
    }
    private void LateUpdate()
    {
        updatePosAndRot();
    }
}
