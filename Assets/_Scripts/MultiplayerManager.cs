using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerManager : NetworkBehaviour
{
    public static MultiplayerManager Instance { get; private set; }
    [SerializeField] private NetworkList<PlayerData> m_PlayerDataNetworkList;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        m_PlayerDataNetworkList = new NetworkList<PlayerData>();
        m_PlayerDataNetworkList.OnListChanged += PlayerDataNetwork_OnListChanged;
    }

    private void PlayerDataNetwork_OnListChanged(NetworkListEvent<PlayerData> changeEvent)
    {

    }

    #region server
    public void StartHost()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback -= NetworkManager_ConnectionApprovalCallback;
        NetworkManager.Singleton.ConnectionApprovalCallback += NetworkManager_ConnectionApprovalCallback;

        NetworkManager.Singleton.OnClientConnectedCallback -= NetworkManager_OnClientConnectedCallback;
        NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;

        NetworkManager.Singleton.OnClientDisconnectCallback -= NetworkManager_Server_OnClientDisconnectCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_Server_OnClientDisconnectCallback;
        NetworkManager.Singleton.StartHost();

    }
    private void NetworkManager_OnClientConnectedCallback(ulong clientId)
    {
        m_PlayerDataNetworkList.Add(new PlayerData
        {
            clientId = clientId
        });

        SetPlayerIdServerRpc(AuthenticationService.Instance.PlayerId);
    }
    private void NetworkManager_Server_OnClientDisconnectCallback(ulong clientId)
    {

    }

    public int GetPlayerDataIndexFromClientId(ulong clientId)
    {
        for (int i = 0; i < m_PlayerDataNetworkList.Count; i++)
        {
            if (m_PlayerDataNetworkList[i].clientId == clientId)
            {
                return i;
            }
        }
        return -1;
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetPlayerIdServerRpc(string playerId, ServerRpcParams serverRpcParams = default) 
    {
        int playerDataIndex = GetPlayerDataIndexFromClientId(serverRpcParams.Receive.SenderClientId);
        PlayerData playerData = m_PlayerDataNetworkList[playerDataIndex];
        playerData.playerId = playerId;
        m_PlayerDataNetworkList[playerDataIndex] = playerData;
    }


    private static void NetworkManager_ConnectionApprovalCallback(NetworkManager.ConnectionApprovalRequest arg1, NetworkManager.ConnectionApprovalResponse arg2)
    {
        
    }

    #endregion

    #region client

    public void StartClient() 
    {
        NetworkManager.Singleton.OnClientDisconnectCallback -= NetworkManager_Client_OnClientDisconnectCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_Client_OnClientDisconnectCallback;
        NetworkManager.Singleton.OnClientConnectedCallback -= NetworkManager_Client_OnClientConnectedCallback;
        NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_Client_OnClientConnectedCallback;
        NetworkManager.Singleton.StartClient();
    }



    private void NetworkManager_Client_OnClientConnectedCallback(ulong clientId)
    {
        SetPlayerIdServerRpc(AuthenticationService.Instance.PlayerId);
    }

    private void NetworkManager_Client_OnClientDisconnectCallback(ulong clientId)
    {
        if (LobbyManager.JoinedLobby.HostId == clientId.ToString()) 
        {
            Disconnect();
        }
    }
    #endregion
    public void Disconnect()
    {

        LobbyManager.LeaveLobby();
            


        NetworkManager.Singleton.Shutdown();

        CustomSceneManager.LoadScene(0);

    }



}

