using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Cinemachine;

public class CharacterSelectionUI : MonoBehaviour
{
    public CinemachineBrain m_CinemachineBrain;
    public CinemachineVirtualCamera m_MainMenuCamera;
    public CinemachineVirtualCamera m_CharacterSelectionCamera;

    public GameObject m_CanvasToActivate;
    void Awake()
    {

    }

    void Start()
    {

    }

    public void OnBackButtonClicked() 
    {
        m_CharacterSelectionCamera.Priority = 10;
        m_MainMenuCamera.Priority = 100;
        m_CanvasToActivate.SetActive(false);
    }

    public void OnCameraActivated(ICinemachineCamera activatedCamera, ICinemachineCamera deactivatedCamera)
    {
        if (activatedCamera.VirtualCameraGameObject != m_CharacterSelectionCamera.gameObject)
            return;
        StartCoroutine(Coroutine(activatedCamera));
    }

    IEnumerator Coroutine(ICinemachineCamera activatedCamera)
    {
        yield return new WaitUntil(() => !m_CinemachineBrain.IsBlending);
        Debug.Log("Camera transition completed!"); // Replace with your desired logic
        m_CanvasToActivate.SetActive(true);


    }
}

