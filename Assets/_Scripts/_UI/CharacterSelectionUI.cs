using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class CharacterSelectionUI : MonoBehaviour
{
    [Header("Cameras")]
    public CinemachineVirtualCamera m_MainMenuCamera;
    public CinemachineVirtualCameraBase m_CreateGameCamera;

    [Header("UI Elements")]
    public Button m_BackButton;
    public Button m_ConfirmButton;

    public GameObject m_NavigationUIObject;
    public GameObject m_CreateGameUIObject;

    void Awake()
    {

    }
    private void OnEnable()
    {
        m_BackButton.onClick.AddListener(() => OnBackButtonClicked());
        m_ConfirmButton.onClick.AddListener(() => OnConfirmButtonClicked());
    }

    private void OnDisable()
    {
        m_BackButton.onClick.RemoveAllListeners();
        m_ConfirmButton.onClick.RemoveAllListeners();
    }
    void Start()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            NetworkManager.Singleton.StartClient();

    }
    public void OnBackButtonClicked()
    {
        m_NavigationUIObject.SetActive(false);
        CinemachineCameraSwitcher.ActivateCamera(m_MainMenuCamera);

    }

    public void OnConfirmButtonClicked() 
    {
        m_NavigationUIObject.SetActive(false);
        m_CreateGameUIObject.SetActive(true);
    }
}

