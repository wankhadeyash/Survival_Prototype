using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.Services.Lobbies.Models;

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

    [Header("Current Lobby Info")]
    [SerializeField] private TextMeshProUGUI m_CurrentLobbyNameText;
    [SerializeField] private TextMeshProUGUI m_CurrentLobbyCodeText;
    [SerializeField] private Button m_LeaveCurrentLobbyButton;

    [Header("Join Lobby with Code")]
    [SerializeField] private TMP_InputField m_LobbyCodeIF;
    [SerializeField] private Button m_JoinLobbyWithCodeButton;


    public GameObject m_CreateGameUIObject;
    public GameObject m_NavigationUIObject;

    void Awake()
    {
  
    }

    private void OnEnable()
    {
        LobbyManager.OnLobbyJoined += OnLobbyJoined;
        LobbyManager.OnLobbyLeft += OnLobbyLeft;
        //m_CreateLobbyButton.onClick.AddListener(() => OnCreateLobbyButtonClicked());
        //m_StartGameButton.onClick.AddListener(() => OnStartGameButtonClicked());
        //m_QuickJoinLobbyButton.onClick.AddListener(() => OnQuickJoinLobbyButtonClicked());

        m_CreateButton.onClick.AddListener(() => OnCreateLobbyButtonClicked());
        m_CloseButton.onClick.AddListener(() => OnCloseButtonClicked());
        m_LeaveCurrentLobbyButton.onClick.AddListener(() => OnLeaveLobbyButtonClicked());
        m_JoinLobbyWithCodeButton.onClick.AddListener(() => OnJoinLobbyWithCodeButtonClicked());

    }

    

    private void OnDisable()
    {
        LobbyManager.OnLobbyJoined -= OnLobbyJoined;
        LobbyManager.OnLobbyLeft-= OnLobbyLeft;

        //m_CreateLobbyButton.onClick.RemoveAllListeners();
        //m_StartGameButton.onClick.RemoveAllListeners();
        //m_QuickJoinLobbyButton.onClick.RemoveAllListeners();


        m_CloseButton.onClick.RemoveAllListeners();
        m_CreateButton.onClick.RemoveAllListeners();
        m_LeaveCurrentLobbyButton.onClick.RemoveAllListeners();
        m_JoinLobbyWithCodeButton.onClick.RemoveAllListeners();
    }


    private void OnLobbyJoined(ClientType clientType, Lobby joinedLobby)
    {
        m_CurrentLobbyNameText.text = joinedLobby.Name;
        m_CurrentLobbyCodeText.text = joinedLobby.LobbyCode;

        m_LeaveCurrentLobbyButton.gameObject.SetActive(true);

    }

    private void OnLobbyLeft()
    {
        m_CurrentLobbyNameText.text = "";
        m_CurrentLobbyCodeText.text = "";

        m_LeaveCurrentLobbyButton.gameObject.SetActive(false);
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


    public void OnLeaveLobbyButtonClicked()
    {
        LobbyManager.LeaveLobby();
    }

    public void OnJoinLobbyWithCodeButtonClicked() 
    {
        if (string.IsNullOrEmpty(m_LobbyCodeIF.text))
            return;

        //LobbyManager.JoinWithCode(m_LobbyCodeIF.text);
    }
}

