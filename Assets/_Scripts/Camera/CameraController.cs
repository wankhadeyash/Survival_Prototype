using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class CameraController : SingletonBase<CameraController>
{
    
    [SerializeField] private CinemachineVirtualCameraBase m_VirtualCamera;
    private float m_ShakeTimer = 0;
    private float m_ShakeTimerTotal = 0;
    private float m_StartingIntensity = 0;

    private void Update()
    {
        ShakeCameraForGivenTime();
    }

    private void ShakeCameraForGivenTime() 
    {
        if (m_ShakeTimer > 0) 
        {
            m_ShakeTimer -= Time.deltaTime;
            if (m_ShakeTimer <= 0) 
            {
                //Timer over
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                m_VirtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(m_StartingIntensity, 0, 1 - m_ShakeTimer / m_ShakeTimerTotal);

            }
        }
    }
    public void SetCameraFollowAnfLookAtInternal(Transform follow, Transform lookAt)
    {
        m_VirtualCamera.LookAt = lookAt;
        m_VirtualCamera.Follow = follow;

    }

    public void StartCameraShake(float intensity, float time) 
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
           m_VirtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        m_StartingIntensity = intensity;
        m_ShakeTimer = time;
        m_ShakeTimerTotal = time;
    }

    public  void SetCameraFollow(Transform follow) 
    {
        m_VirtualCamera.Follow = follow;

    }

    public void SetCameraLook(Transform looAt) 
    {
        m_VirtualCamera.LookAt = looAt;
    }
}

