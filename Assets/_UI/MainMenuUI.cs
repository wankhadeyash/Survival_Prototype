using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Cinemachine;
public class MainMenuUI : MonoBehaviour
{
    public CinemachineVirtualCamera m_ChatacterSelectionCamera;
    public GameObject m_CanvasToActivate;


    void Awake()
    {

    }

    void Start()
    {

    }

    public void OnStartButtonClicked()
    {
        CinemachineCameraSwitcher.ActivateCamera(m_ChatacterSelectionCamera, ()=> { m_CanvasToActivate.SetActive(true); });
    }

    public void OnSettignsButtonClicked() { }

    public void OnQuitButtonClicked() { }

    

}

