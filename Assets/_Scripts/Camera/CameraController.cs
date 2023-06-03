using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class CameraController : SingletonBase<CameraController>
{
    [SerializeField] private CinemachineVirtualCameraBase m_VirtualCamera;

    public static void SetCameraFollowAndLookAt(Transform follow, Transform lookAt)
    {
        Instance.SetCameraFollowAnfLookAtInternal(follow, lookAt);
    }

    private void SetCameraFollowAnfLookAtInternal(Transform follow, Transform lookAt)
    {
        m_VirtualCamera.LookAt = lookAt;
        m_VirtualCamera.Follow = follow;

    }

}

