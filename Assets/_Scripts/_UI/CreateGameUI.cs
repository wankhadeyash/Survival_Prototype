using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CreateGameUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button m_CreateLobbyButton;
    [SerializeField] private Button m_QuickJoinLobbyButton;
    [SerializeField] private Button m_StartGameButton;
    [SerializeField] private Image m_WaitingForHost;
    void Awake()
    {
        m_StartGameButton.gameObject.SetActive(false);
        m_WaitingForHost.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        m_CreateLobbyButton.onClick.AddListener(() => OnCreateLobbyButtonClicked());
        m_StartGameButton.onClick.AddListener(() => OnStartGameButtonClicked());
        m_QuickJoinLobbyButton.onClick.AddListener(() => OnQuickJoinLobbyButtonClicked());

        LobbyManager.OnLobbyJoined += LobbyManager_OnLobbyJoined;
    }

   

    private void OnDisable()
    {
        m_CreateLobbyButton.onClick.RemoveAllListeners();
        m_StartGameButton.onClick.RemoveAllListeners();
        m_QuickJoinLobbyButton.onClick.RemoveAllListeners();
    }

    void Start()
    {

    }

    public void OnCreateLobbyButtonClicked() 
    {
        LobbyManager.CreateLobby(inputField.text, false);
    }

    public void OnQuickJoinLobbyButtonClicked() 
    {
        LobbyManager.QuickJoin();
    }

    public void OnStartGameButtonClicked() 
    {
        CustomSceneManager.LoadScene(1,()=> { MultiplayerManager.JoinHost(); });
    }

    private void LobbyManager_OnLobbyJoined(ClientType clientType)
    {
        if (clientType == ClientType.Host)
            m_StartGameButton.gameObject.SetActive(true);
        else
            m_WaitingForHost.gameObject.SetActive(true);
    }
}

