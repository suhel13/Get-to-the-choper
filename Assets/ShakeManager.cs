using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeManager : MonoBehaviour
{
    public static ShakeManager Instance { get; private set; }
    CinemachineVirtualCamera vCam;
    float shakeTimer;
    float startShakeTimer;
    float startIntencity;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        vCam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        ShakeUpdate();
    }

    public void CameraShake(float intecity, float duration)
    {
        if (startIntencity < intecity)
        {

            startIntencity = intecity;
            shakeTimer = duration;
            startShakeTimer = duration;

            vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intecity;
        }
    }

    void ShakeUpdate()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Mathf.Lerp(startIntencity, 0f, 1 - shakeTimer / startShakeTimer);
        }
        else
        {
            startIntencity = 0;
            vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
        }
    }
}
