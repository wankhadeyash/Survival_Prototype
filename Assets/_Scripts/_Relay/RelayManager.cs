using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Services;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using System;
using Unity.Networking.Transport.Relay;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using System.Threading.Tasks;

public class RelayManager : SingletonBase<RelayManager>
{
    private bool m_IsConnected;
    public bool IsConnected => m_IsConnected;
    private void OnEnable()
    {
        LobbyManager.OnUnityAuthenticationSuccesfull += OnUnityAuthenticationSuccesfull;
    }

    private void OnDisable()
    {
        LobbyManager.OnUnityAuthenticationSuccesfull -= OnUnityAuthenticationSuccesfull;

    }

    void Start()
    {

    }

    private void Update()
    {
    }

    private void OnUnityAuthenticationSuccesfull()
    {
    
    }

    public static async Task<string> CreateRelay() 
    {
        return await Instance.CreateRelayInternal();
    }

    private async  Task<string> CreateRelayInternal() 
    {
        Debug.Log("Creating relay");
        try
        {
            Allocation allocation =   await RelayService.Instance.CreateAllocationAsync(3);
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
            Debug.Log("Join code is" + joinCode);

            CustomSceneManager.LoadSceneOnNetwork(SceneInfo.MainWorld);
            m_IsConnected = true;

            return joinCode;

        }
        catch (RelayServiceException e)
        
        {
            Debug.Log(e);
            return null;
        }
    }

    public static void JoinRelay(string joinCode) 
    {
        Instance.JoinRelayInternal(joinCode);
    }

    private async void JoinRelayInternal (string joinCode) 
    {
        try
        {
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            
            m_IsConnected = true;
        }

        catch (RelayServiceException e)
        {

            Debug.Log(e);
        }
    }

}

