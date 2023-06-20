using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class CameraController : SingletonBase<CameraController>
{
    
    [SerializeField] private CinemachineVirtualCameraBase m_VirtualCamera;


    public void SetCameraFollowAnfLookAtInternal(Transform follow, Transform lookAt)
    {
        m_VirtualCamera.LookAt = lookAt;
        m_VirtualCamera.Follow = follow;

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

