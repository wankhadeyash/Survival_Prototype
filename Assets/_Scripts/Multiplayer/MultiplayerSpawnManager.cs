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
    public IEnumerator SpawnAvatarOnDemand(int avatarIndex) 
    {
        yield return new WaitForSeconds(10f);
        //SpawnAvatarOnDemandServerRPC(avatarIndex);

    }

    //private void SpawnAvatarOnDemandServerRPC(int avatarIndex, ServerRpcParams serverRpcParams = default) 
    //{
    //    //Get player object
    //    AvatarData avatar = PlayerDataManager.Instance.AvatarList.avatars[avatarIndex];
    //    NetworkClient client = NetworkManager.Singleton.ConnectedClients[serverRpcParams.Receive.SenderClientId];
    //    NetworkObject playerObj = client.PlayerObject;

    //    //Get avatar object spawned
    //    GameObject geometry = playerObj.GetComponent<PlayerController>().Geometry;

    //    GameObject avatarObj = Instantiate(PlayerDataManager.Instance.AvatarList.avatars[avatarIndex].prefab,geometry.transform.position,
    //        geometry.transform.rotation);
    //    NetworkObject avatarNetworkObj = avatarObj.GetComponent<NetworkObject>();
    //    avatarNetworkObj.Spawn();
    //    avatarNetworkObj.TrySetParent(playerObj);
    //    avatarNetworkObj.transform.position = playerObj.transform.position;
    //    avatarNetworkObj.transform.rotation = playerObj.transform.rotation;

    //}
    [ServerRpc(RequireOwnership = false)]
    void SpawnplayerServerRPC(int avatarIndex, ServerRpcParams serverRpcParams = default) 
    {

        GameObject player = Instantiate(PlayerDataManager.Instance.AvatarList.avatars[avatarIndex].networkPrefab);
        player.GetComponent<NetworkObject>().SpawnAsPlayerObject(serverRpcParams.Receive.SenderClientId,false);
        
    }

}

