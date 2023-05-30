using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Cinemachine;
public class MainMenuUI : MonoBehaviour
{
    public CinemachineVirtualCamera m_MainMenuCamera;
    public CinemachineVirtualCamera m_ChatacterSelectionCamera;

    public CinemachineBrain m_Brain;

    void Awake()
    {

    }

    void Start()
    {

    }

    public void OnStartButtonClicked()
    {
        m_MainMenuCamera.Priority = 10;
        m_ChatacterSelectionCamera.Priority = 100;
    }

    public void OnSettignsButtonClicked() { }

    public void OnQuitButtonClicked() { }

    

}

