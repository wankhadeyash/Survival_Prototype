using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CreateGameUI : MonoBehaviour
{
    [Header("Create Lobby")]
    [SerializeField] private TMP_InputField m_LobbyNameIF;
    [SerializeField] private Toggle m_LobbyTypeToggle;

    //[SerializeField] private Button m_CreateLobbyButton;
    //[SerializeField] private Button m_QuickJoinLobbyButton;
    //[SerializeField] private Button m_StartGameButton;
    [SerializeField] private Button m_CloseButton;
    [SerializeField] private Button m_CreateButton;


    public GameObject m_CreateGameUIObject;
    public GameObject m_NavigationUIObject;

    void Awake()
    {
  
    }

    private void OnEnable()
    {
        //m_CreateLobbyButton.onClick.AddListener(() => OnCreateLobbyButtonClicked());
        //m_StartGameButton.onClick.AddListener(() => OnStartGameButtonClicked());
        //m_QuickJoinLobbyButton.onClick.AddListener(() => OnQuickJoinLobbyButtonClicked());

        m_CreateButton.onClick.AddListener(() => OnCreateLobbyButtonClicked());
        m_CloseButton.onClick.AddListener(() => OnCloseButtonClicked());

    }

   

    private void OnDisable()
    {
        //m_CreateLobbyButton.onClick.RemoveAllListeners();
        //m_StartGameButton.onClick.RemoveAllListeners();
        //m_QuickJoinLobbyButton.onClick.RemoveAllListeners();


        m_CloseButton.onClick.RemoveAllListeners();
        m_CreateButton.onClick.RemoveAllListeners();
    }

    void Start()
    {

    }

    public void OnCreateLobbyButtonClicked() 
    {
        LobbyManager.CreateLobby(m_LobbyNameIF.text, m_LobbyTypeToggle.isOn);
    }

    public void OnQuickJoinLobbyButtonClicked() 
    {
        LobbyManager.QuickJoin();
    }

    public void OnStartGameButtonClicked() 
    {
        CustomSceneManager.LoadScene(1,()=> { MultiplayerManager.JoinHost(); });
    }

    public void OnCloseButtonClicked() 
    {
        m_CreateGameUIObject.SetActive(false);
        m_NavigationUIObject.SetActive(true);
    }

}

