using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using System.Threading.Tasks;
using System;
using Sirenix.OdinInspector;

public enum ClientType 
{
    Host,
    Client
}
public class LobbyManager : SingletonBase<LobbyManager>
{
    private Lobby joinedLobby, m_HostLobby;
    public static Action<ClientType> OnLobbyJoined;
    public static Action OnUnityAuthenticationSuccesfull;
    public static Action OnLobbyCreated;
    private float m_HearbeatTimer;

    public bool m_IsAuthenticated;

    protected override void OnAwake()
    {
        InitializeUnityAuthentication();
    }


    private void Update()
    {
        HandleLobbyHeartBeat();
    }
    private async void InitializeUnityAuthentication()
    {
        if (UnityServices.State != ServicesInitializationState.Initialized)
        {
            InitializationOptions initializationOptions = new InitializationOptions();
            initializationOptions.SetProfile(UnityEngine.Random.Range(0, 10000).ToString());
            await UnityServices.InitializeAsync(initializationOptions);

            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            m_IsAuthenticated = true;
            OnUnityAuthenticationSuccesfull?.Invoke();
        }
    }

    [Button]
    public static void CreateLobby(string lobbyName, bool isPrivate) 
    {
        Instance.CreateLobbyInternal(lobbyName, isPrivate);
    }

    private async void CreateLobbyInternal(string lobbyName, bool isPrivate)
    {
        try
        {
            m_HostLobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, 10, new CreateLobbyOptions
            {
                
                IsPrivate = isPrivate
            });

            joinedLobby = m_HostLobby;

        }
        catch (LobbyServiceException e) 
        {
            Debug.Log(e);
        }

        Debug.Log($"Lobby created with {joinedLobby.Name} {joinedLobby.Id} {joinedLobby.LobbyCode}");
        OnLobbyCreated?.Invoke();
        OnLobbyJoined?.Invoke(ClientType.Host);
    }


    public static void QuickJoin() 
    {
        Instance.QuickJoinInternal();
    }
    public async void QuickJoinInternal() 
    {
        try
        {
            joinedLobby = await LobbyService.Instance.QuickJoinLobbyAsync();
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }

        OnLobbyJoined?.Invoke(ClientType.Client);

    }

    [Button]
    public async Task<List<Lobby>> GetLobbiesList()
    {
        return await Instance.GetLobbiesListInternal();
    }
    private async Task<List<Lobby>> GetLobbiesListInternal() 
    {
        QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();

        Debug.Log($"Lobbies found {queryResponse.Results.Count}");
        foreach (Lobby lobby in queryResponse.Results) 
        {
            Debug.Log($"{lobby.Name} {lobby.Id} {lobby.LobbyCode}");
        }
        return queryResponse.Results;

    }

    public static void JoinLobby() 
    {
        
    }

    private async void JoinLobbyInternal() 
    {
        try
        {

        }
        catch { }
    }

    private void HandleLobbyHeartBeat() 
    {
        if (m_HostLobby != null) 
        {
            m_HearbeatTimer -= Time.deltaTime;
            if (m_HearbeatTimer < 0)
            {
                float heartbeatTimerMax = 15;
                m_HearbeatTimer = heartbeatTimerMax;

                LobbyService.Instance.SendHeartbeatPingAsync(m_HostLobby.Id);
            }   
        }
    }

}

