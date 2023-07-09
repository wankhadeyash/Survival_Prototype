using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;



public class MultiplayerSpawnManager : NetworkBehaviour
{

    public static MultiplayerSpawnManager Instance;
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {

    }

    public void SpawnAvatarOnDemand(int avatarIndex) 
    {
        SpawnAvatarOnDemandServerRPC(avatarIndex);


    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnAvatarOnDemandServerRPC(int avatarIndex, ServerRpcParams serverRpcParams = default) 
    {
        AvatarData avatar = PlayerDataManager.Instance.AvatarList.avatars[avatarIndex];
        NetworkClient client = NetworkManager.Singleton.ConnectedClients[serverRpcParams.Receive.SenderClientId];
        NetworkObject playerObj = client.PlayerObject;

        GameObject avatarObj = Instantiate(PlayerDataManager.Instance.AvatarList.avatars[avatarIndex].prefab);
        NetworkObject avatarNetworkObj = avatarObj.GetComponent<NetworkObject>();
        avatarNetworkObj.Spawn(true);
    }

}

