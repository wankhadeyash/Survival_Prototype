using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;




public class NetworkGameManager : NetworkBehaviour
{
    [SerializeField] GameObject m_PlayerPrefab;
    GameObject m_NetworkPlayer;

    public static NetworkGameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Set the initial game state to Playing when the game starts.
   
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;
            NetworkManager.Singleton.SceneManager.OnLoadComplete += SceneManager_OnLoadEventCompleted;
        }
        GameManager.SetGameState(GameState.Playing);
    }

    private void SceneManager_OnLoadEventCompleted(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
    {
        m_NetworkPlayer = Instantiate(m_PlayerPrefab);
        m_NetworkPlayer.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
    }

    private void NetworkManager_OnClientDisconnectCallback(ulong clientId)
    {
        Debug.Log($"{clientId } is disconnected");
        m_NetworkPlayer.GetComponent<NetworkObject>().Despawn(true);
    }


}
