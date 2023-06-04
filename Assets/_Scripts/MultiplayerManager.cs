using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


public class MultiplayerManager : SingletonBase<MultiplayerManager>
{
    private void OnEnable()
    {

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

