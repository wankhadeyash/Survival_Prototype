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
    [SerializeField] private Transform m_PlayerPrefab;
    [SerializeField] private GameObject m_Geometry;


    public static MultiplayerSpawnManager Instance;
    private void OnEnable()
    {
        MultiplayerManager.OnConnectedToNetworkManager += OnConnectedToNetworkManager;
    }

   

    private void OnDisable()
    {

        MultiplayerManager.OnConnectedToNetworkManager -= OnConnectedToNetworkManager;

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {

    }

    public override void OnNetworkSpawn()
    {
        //StartCoroutine(SpawnAvatarOnDemand(PlayerDataManager.Instance.m_Data.avatarIndex));
        SpawnplayerServerRPC(PlayerDataManager.Instance.m_Data.avatarIndex);

    }
    private void Update()
    {
 
    }

    private void OnConnectedToNetworkManager()
    {

    }


    [ServerRpc(RequireOwnership = false)]
    void SpawnplayerServerRPC(int avatarIndex, ServerRpcParams serverRpcParams = default) 
    {

        GameObject player = Instantiate(PlayerDataManager.Instance.AvatarList.avatars[avatarIndex].networkPrefab);
        player.GetComponent<NetworkObject>().SpawnAsPlayerObject(serverRpcParams.Receive.SenderClientId,false);
        
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetObjectsParentServerRPC(NetworkObjectReference objectToSetNetworkRef, NetworkObjectReference parentNetworkRef, ServerRpcParams serverRpcParams = default) 
    {
        if (objectToSetNetworkRef.TryGet(out NetworkObject objectToSet))
        {
            if (parentNetworkRef.TryGet(out NetworkObject parentObject))
            {
                objectToSet.TrySetParent(parentObject);
            }
            else 
            {
                Debug.LogError($"cannot retrive networkobject from reference of parent object");

            }
        }
        else 
        {
            Debug.LogError($"cannot retrive networkobject from reference of object to set");
        }
    }

}

