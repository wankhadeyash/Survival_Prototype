using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerManager : MonoBehaviour
{

    private PlayerData m_PlayerData;
    public PlayerData PlayerData => m_PlayerData;
    public static MultiplayerManager Instance { get; private set; }
    public static Action OnNetworkManager_Shutdown;

    private void OnEnable()
    {

        LobbyManager.OnUnityAuthenticationSuccesfull += LobbyManager_AuthenticationSuccesfull;

        LobbyManager.OnCreateLobbyFailed += LobbyManager_OnCreateLobbyFailed;
        LobbyManager.OnQuickJoinLobbyFailed += LobbyManager_OnJoinLobbyFailed;
        LobbyManager.OnJoinLobbyWithCodeFailed += LobbyManager_OnJoinLobbyFailed;
        LobbyManager.OnJoinLobbyWithIDFailed += LobbyManager_OnJoinLobbyFailed;

     
    }
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);
        m_PlayerData = new PlayerData();


    }
    private void LobbyManager_AuthenticationSuccesfull()
    {
        m_PlayerData.playerId = AuthenticationService.Instance.PlayerId;
    }

    private void OnDisable()
    {
        LobbyManager.OnCreateLobbyFailed -= LobbyManager_OnCreateLobbyFailed;
        LobbyManager.OnQuickJoinLobbyFailed -= LobbyManager_OnJoinLobbyFailed;
        LobbyManager.OnJoinLobbyWithCodeFailed -= LobbyManager_OnJoinLobbyFailed;
        LobbyManager.OnJoinLobbyWithIDFailed -= LobbyManager_OnJoinLobbyFailed;

    }
    private void LobbyManager_OnCreateLobbyFailed(string obj)
    {
        Disconnect();
    }
    private void LobbyManager_OnJoinLobbyFailed(string obj)
    {
        Disconnect();
    }

   

   

    private void Update()
    {
     
    }


    private void NetworkManager_OnClientConnectedCallback(ulong clientId)
    {
        Debug.Log($"Player id is {AuthenticationService.Instance.PlayerId}");
        //if(clientId.ToString() == AuthenticationService.Instance.PlayerId)
            //CustomSceneManager.LoadSceneOnNetwork(SceneInfo.MainWorld);


    }
    private void NetworkManager_OnClientDisconnectCallback(ulong clientId)
    {
        if (clientId == 0)
            Disconnect();
    }


    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        m_PlayerData.clientId = NetworkManager.Singleton.LocalClientId;
        NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;
    }

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        m_PlayerData.clientId = NetworkManager.Singleton.LocalClientId;
        NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;
    }

    public void Disconnect()
    {
       NetworkManager.Singleton.Shutdown();
       LobbyManager.Instance.LeaveLobby();
       OnNetworkManager_Shutdown?.Invoke();
    }

}

