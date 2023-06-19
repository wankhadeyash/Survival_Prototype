using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerManager : NetworkBehaviour
{
    [SerializeField] private GameObject m_PlayerPrefab;
    public static MultiplayerManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void StartHost() 
    {
        Instance.StartHostInternal();
    }
    private void StartHostInternal()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectedCallback;



        NetworkManager.Singleton.StartHost();
    }

    private void SceneManager_OnLoadEventCompleted(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        if (IsServer)
        {
            if (sceneName == SceneInfo.MainWorld.ToString())
            {
                foreach (ulong cliendId in NetworkManager.Singleton.ConnectedClientsIds) 
                {
                    GameObject player = Instantiate(m_PlayerPrefab);
                    player.GetComponent<NetworkObject>().SpawnAsPlayerObject(cliendId, true);

                }
            }
        }
    }

    private void NetworkManager_OnClientConnectedCallback(ulong obj)
    {

    }
    private void NetworkManager_OnClientDisconnectedCallback(ulong obj)
    {

    }
    public static void StartClient() 
    {
        Instance.StartClientInternal();
    }

    private void StartClientInternal() 
    {
        NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectedCallback;
        NetworkManager.Singleton.StartClient();
    }

}

