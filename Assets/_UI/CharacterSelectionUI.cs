using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CharacterSelectionUI : MonoBehaviour
{
    public CinemachineVirtualCamera m_MainMenuCamera;
    public GameObject m_CanvasToDeactivate;

    void Awake()
    {

    }

    void Start()
    {

    }

    public void OnBackButtonClicked()
    {
        m_CanvasToDeactivate.SetActive(false);
        CinemachineCameraSwitcher.ActivateCamera(m_MainMenuCamera);

    }

    public void OnConfirmButtonClicked() 
    {
        SceneManager.LoadScene(1);
    }
}

