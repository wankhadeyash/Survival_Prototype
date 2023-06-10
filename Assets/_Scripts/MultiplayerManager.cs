using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerManager : SingletonBase<MultiplayerManager>
{
    NetworkManager m_NetworkManager;
    private void OnEnable()
    {
        m_NetworkManager = FindObjectOfType<NetworkManager>();
     
        m_NetworkManager.OnServerStarted += OnServerStarted;
    }

    private void OnDisable()
    {
        m_NetworkManager.OnServerStarted -= OnServerStarted;
    }

    private void OnServerStarted()
    {
        CustomSceneManager.LoadSceneOnNetwork(SceneInfo.MainWorld);
        GameManager.SetGameState(GameState.Playing);
    }

    void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_Client_OnClientConnectedCallback;

    }

    private void NetworkManager_Client_OnClientConnectedCallback(ulong clientId)
    {
        Debug.Log(clientId);
    }

    public static void JoinHost() 
    {
        Instance.JoinHostInternal();
    }

    private void JoinHostInternal() 
    {
        NetworkManager.Singleton.StartHost();
    }

    public static void JoinClient() 
    {
        Instance.JoinClientInternal();
    }

    private void JoinClientInternal() 
    {
        NetworkManager.Singleton.StartClient();
    }
}

