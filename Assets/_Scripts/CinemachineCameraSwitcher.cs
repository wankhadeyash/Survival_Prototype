using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class CinemachineCameraSwitcher : SingletonBase<CinemachineCameraSwitcher>
{
    public CinemachineBrain m_CinemachineBrain;
    private CinemachineVirtualCameraBase m_CurrentActiveCamera;

    void Start()
    {
        m_CurrentActiveCamera = GetCurrentActiveCinemachineCamera();
    }

    public static CinemachineVirtualCameraBase GetCurrentActiveCinemachineCamera() 
    {
        return Instance.GetCurrentActiveCinemachineCameraInternal();
    }

    private CinemachineVirtualCameraBase GetCurrentActiveCinemachineCameraInternal()
    {
        if (m_CinemachineBrain != null && m_CinemachineBrain.ActiveVirtualCamera != null)
        {
            return m_CinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCameraBase>();
        }

        return null;
    }




    public static void ActivateCamera(CinemachineVirtualCameraBase camera, System.Action onCameraTransitionFinished = null) 
    {
        Instance.ActivateCameraInternal(camera, onCameraTransitionFinished);
    }


    private void ActivateCameraInternal(CinemachineVirtualCameraBase camera, System.Action onCameraTransitionFinished = null)
    {
        if (m_CurrentActiveCamera != null)
        {
            m_CurrentActiveCamera.Priority = 0; // Lower the priority of the current active camera
        }

        camera.Priority = 10; // Set the priority of the new camera to a higher value to activate it

        m_CurrentActiveCamera = camera;

        if (onCameraTransitionFinished != null)
        {
            StartCoroutine(WaitForCameraTransition(onCameraTransitionFinished));
        }
    }

    private IEnumerator WaitForCameraTransition(System.Action onCameraTransitionFinished)
    {
        yield return new WaitUntil(() => m_CinemachineBrain.IsBlending); // Wait until camera transition is finished
        yield return new WaitWhile(() => m_CinemachineBrain.IsBlending); // Wait until camera transition is finished

        onCameraTransitionFinished?.Invoke(); // Invoke the action when camera transition is finished
    }
}
