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
    public const string KEY_START_GAME = "Start Game Key";

    private Lobby joinedLobby, m_HostLobby;
    public static Lobby JoinedLobby => Instance.joinedLobby;

    public static Action<ClientType, Lobby> OnLobbyJoined;
    public static Action OnGameStarted;
    public static Action OnLobbyLeft;
    public static Action OnUnityAuthenticationSuccesfull;
    public static Action OnLobbyCreated;
    private float m_HearbeatTimer;
    private float m_LobbyUpdateTimer;

    public bool m_IsAuthenticated;


    protected override void OnAwake()
    {
        InitializeUnityAuthentication();
    }


    private void Update()
    {
        HandleLobbyHeartBeat();
        HandleLobbyPollForUpdates();
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

                IsPrivate = isPrivate,
                Data = new Dictionary<string, DataObject>
                {
                    { KEY_START_GAME, new DataObject(DataObject.VisibilityOptions.Member, "0")  }
                }
            });

            joinedLobby = m_HostLobby;

        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }

        Debug.Log($"Lobby created with {joinedLobby.Name} {joinedLobby.Id} {joinedLobby.LobbyCode}");
        OnLobbyCreated?.Invoke();
        OnLobbyJoined?.Invoke(ClientType.Host, joinedLobby);
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

        OnLobbyJoined?.Invoke(ClientType.Client, joinedLobby);

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

    public static void LeaveLobby()
    {
        Instance.LeaveLobbyInternal();
    }
    private async void LeaveLobbyInternal()
    {
        try
        {
            //Ensure you sign-in before calling Authentication Instance
            //See IAuthenticationService interface
            string playerId = AuthenticationService.Instance.PlayerId;
            await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, playerId);

            if (m_HostLobby != null)
                m_HostLobby = null;
            joinedLobby = null;
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
        OnLobbyLeft?.Invoke();


    }

    public static Task<string> JoinWithCode(string lobbyCode)
    {
        return Instance.JoinLobbyWithCodeInternal(lobbyCode);
    }

    private async Task<string> JoinLobbyWithCodeInternal(string lobbyCode)
    {
        try
        {
            joinedLobby = await LobbyService.Instance.JoinLobbyByCodeAsync(lobbyCode);
            OnLobbyJoined?.Invoke(ClientType.Client, joinedLobby);
            return "Success";
        }
        catch (LobbyServiceException e)
        {

            if (e.Message.Contains("InvalidJoinCode") || e.Message.Contains("contains an invalid character") || e.Message.Contains("lobby not found"))
            {
                Debug.Log("Wrong Lobby Code");
                return "Wrong lobby Code";

            }
            else
            {
                // Log other LobbyServiceException errors
                Debug.Log("Lobby service error: " + e.Message);
                return "e.Message";

            }

        }
    }

    public static void StartGame() 
    {
        Instance.StartGameInternal();
    }
    private async void StartGameInternal()
    {
        if (IsLobbyHost())
        {
            try
            {
                Debug.Log("Start Game");
                string relayCode = await RelayManager.CreateRelay();

                Lobby lobby = await Lobbies.Instance.UpdateLobbyAsync(joinedLobby.Id, new UpdateLobbyOptions
                {
                    Data = new Dictionary<string, DataObject>()
                    {
                        { KEY_START_GAME, new DataObject(DataObject.VisibilityOptions.Member, relayCode)}
                    }
                });
                joinedLobby = lobby;

                OnGameStarted?.Invoke();
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
                return;
            }
        }
    }

    public bool IsLobbyHost()
    {
        return AuthenticationService.Instance.PlayerId == joinedLobby.HostId;
    }

    private void HandleLobbyHeartBeat()
    {
        if (m_HostLobby != null)
        {
            if (IsLobbyHost())

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

    private async void HandleLobbyPollForUpdates()
    {
        if (joinedLobby != null)
        {
            m_LobbyUpdateTimer -= Time.deltaTime;
            if (m_LobbyUpdateTimer < 0)
            {
                float lobbyTimeMax = 1.5f;
                m_LobbyUpdateTimer = lobbyTimeMax;

                try
                {
                    Lobby lobby = await LobbyService.Instance.GetLobbyAsync(joinedLobby.Id);
                    joinedLobby = lobby;

                    if (joinedLobby.Data[KEY_START_GAME].Value != "0")
                    {
                        //Start Game
                        if (!IsLobbyHost())
                        {
                            RelayManager.JoinRelay(joinedLobby.Data[KEY_START_GAME].Value);
                        }

                        OnGameStarted?.Invoke();
                    }
                }
                catch (LobbyServiceException e) 
                {
                    Debug.Log(e);
                }
            }

        }
    }
}

