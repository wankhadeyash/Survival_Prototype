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


public enum ClientType 
{
    Host,
    Client
}
public class LobbyManager : SingletonBase<LobbyManager>
{
    private Lobby joinedLobby;
    public static Action<ClientType> OnLobbyJoined;

    protected override void OnAwake()
    {
        InitializeUnityAuthentication();
    }

    private async void InitializeUnityAuthentication()
    {
        if (UnityServices.State != ServicesInitializationState.Initialized)
        {
            InitializationOptions initializationOptions = new InitializationOptions();
            initializationOptions.SetProfile(UnityEngine.Random.Range(0, 10000).ToString());
            await UnityServices.InitializeAsync(initializationOptions);

            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    public static void CreateLobby(string lobbyName, bool isPrivate) 
    {
        Instance.CreateLobbyInternal(lobbyName, isPrivate);
    }

    private async void CreateLobbyInternal(string lobbyName, bool isPrivate)
    {
        try
        {
            joinedLobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, 10, new CreateLobbyOptions
            {
                IsPrivate = isPrivate
            });

        }
        catch (LobbyServiceException e) 
        {
            Debug.Log(e);
        }
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

}

