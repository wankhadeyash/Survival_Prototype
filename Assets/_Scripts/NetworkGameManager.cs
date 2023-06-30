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


    private void OnEnable()
    {

    }

    private void Awake()
    {
        Instance = this;
    }

    // Set the initial game state to Playing when the game starts.

    public override void OnNetworkSpawn()
    {
        SpawnPlayerServerRpc();

    }

  

    public override void OnNetworkDespawn()
    {

    }

    [ServerRpc(RequireOwnership =  false)]
    void SpawnPlayerServerRpc(ServerRpcParams serverRpcParams = default) 
    {
        GameObject player = Instantiate(m_PlayerPrefab);
        player.GetComponent<NetworkObject>().SpawnAsPlayerObject(serverRpcParams.Receive.SenderClientId, true);
        GameManager.SetGameState(GameState.Playing);

    }


    private void NetworkManager_OnClientDisconnectCallback(ulong clientId)
    {
        Debug.Log($"{clientId } is disconnected");
    }


}
