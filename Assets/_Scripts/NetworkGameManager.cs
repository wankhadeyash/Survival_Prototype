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
    }

    private void SceneManager_OnLoadEventCompleted(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
    {
        GameObject playerTransform = Instantiate(m_PlayerPrefab);
        playerTransform.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
        GameManager.SetGameState(GameState.Playing);
    }

    private void NetworkManager_OnClientDisconnectCallback(ulong obj)
    {

    }


}
