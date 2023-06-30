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
using Unity.Netcode;
using Unity.Services.Relay.Models;
using Unity.Services.Relay;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using UnityEngine.SceneManagement;
public enum ClientType
{
    Host,
    Client
}
public class LobbyManager : SingletonBase<LobbyManager>
{
    NetworkManager m_NetworkManager;

    public const string KEY_START_GAME = "Start Game Key";
    private const string KEY_RELAY_JOIN_CODE = "RelayJoinCode";
    private Lobby joinedLobby;
    public static Lobby JoinedLobby => Instance.joinedLobby;

    public static Action OnUnityAuthenticationSuccesfull;

    public static Action OnCreateLobbyStarted;
    public static Action OnCreateLobbySuccess;
    public static Action<string> OnCreateLobbyFailed;

    public static Action OnQuickJoinLobbyStarted;
    public static Action OnQuickJoinLobbySuccess;
    public static Action<string> OnQuickJoinLobbyFailed;
      
    public static Action OnJoinLobbyWithCodeStarted;
    public static Action OnJoinLobbyWithCodeSuccess;
    public static Action<string> OnJoinLobbyWithCodeFailed;
       
    public static Action OnJoinLobbyWithIDStarted;
    public static Action OnJoinLobbyWithIDSuccess;
    public static Action<string> OnJoinLobbyWithIDFailed;

    public static Action<string> OnLobbyLeft;

    public static Action<string> OnKickFromLobby;

    private float m_HearbeatTimer;
    private float m_LobbyUpdateTimer;

    public bool m_IsAuthenticated;

    #region Mono
    protected override void OnAwake()
    {
        m_NetworkManager = FindObjectOfType<NetworkManager>();  
        InitializeUnityAuthentication();
    }

    private void OnEnable()
    {
       
    }



    private void OnDisable()
    {
        

    }

    private void Update()
    {
        HandleLobbyHeartBeat();
    }
    #endregion


   
    private async void InitializeUnityAuthentication()
    {
        if (UnityServices.State != ServicesInitializationState.Initialized)
        {
            InitializationOptions initializationOptions = new InitializationOptions();
            //initializationOptions.SetProfile(UnityEngine.Random.Range(0, 10000).ToString());
            await UnityServices.InitializeAsync(initializationOptions);

            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            m_IsAuthenticated = true;

            OnUnityAuthenticationSuccesfull?.Invoke();
            SceneManager.LoadScene(SceneInfo.MainMenu.ToString());
        }
    }

    #region Lobby Creation and Joining
    public async void CreateLobby(string lobbyName, bool isPrivate)
    {
        LoadingUI.Instance.EnableContainer("Loading World...");



        OnCreateLobbyStarted?.Invoke();
        try
        {
            joinedLobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, 10, new CreateLobbyOptions
            {
                IsPrivate = isPrivate,
            });

            Allocation allocation = await AllocateRelay();

            string relayJoinCode = await GetRelayJoinCode(allocation);

            await LobbyService.Instance.UpdateLobbyAsync(joinedLobby.Id, new UpdateLobbyOptions
            {
                Data = new Dictionary<string, DataObject> {
                    { KEY_RELAY_JOIN_CODE, new DataObject(DataObject.VisibilityOptions.Member,relayJoinCode) }
                }
            });

            Debug.Log($"Creating realy with code  {relayJoinCode}");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "dtls"));
            MultiplayerManager.Instance.StartHost();

           // CustomSceneManager.LoadSceneOnNetwork(SceneInfo.MainWorld);
            OnCreateLobbySuccess?.Invoke();
        }
        catch (LobbyServiceException e)
        {
            string a = e.ToString();
            OnCreateLobbyFailed?.Invoke(e.ToString());
            Debug.LogError(e);
            throw;

        }

        Debug.Log($"Lobby created with {joinedLobby.Name} {joinedLobby.Id} {joinedLobby.LobbyCode}");
    }



    public async  void QuickJoin()
    {
        LoadingUI.Instance.EnableContainer("Loading World...");

        OnQuickJoinLobbyStarted?.Invoke();
        try
        {
            joinedLobby = await LobbyService.Instance.QuickJoinLobbyAsync();

            string relayJoinCode = joinedLobby.Data[KEY_RELAY_JOIN_CODE].Value;
            JoinAllocation joinAllocation = await JoinRelay(relayJoinCode);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));

            MultiplayerManager.Instance.StartClient();

            OnQuickJoinLobbySuccess?.Invoke();
        }
        catch (LobbyServiceException e)
        {
            OnQuickJoinLobbyFailed?.Invoke(e.ToString());
            Debug.Log(e);
            throw;
        }
    }


    public async  Task<string> JoinWithCode(string lobbyCode)
    {
        LoadingUI.Instance.EnableContainer("Loading World...");
        OnJoinLobbyWithCodeStarted?.Invoke();
        try
        {
            joinedLobby = await LobbyService.Instance.JoinLobbyByCodeAsync(lobbyCode);
            string relayJoinCode = joinedLobby.Data[KEY_RELAY_JOIN_CODE].Value;
            JoinAllocation joinAllocation = await JoinRelay(relayJoinCode);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));

            MultiplayerManager.Instance.StartClient();

            OnJoinLobbyWithCodeSuccess?.Invoke();
            return "Success";
        }
        catch (LobbyServiceException e)
        {
            OnJoinLobbyWithCodeFailed?.Invoke(e.ToString());

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

    

    public async void JoinWithId(string lobbyId)
    {
        LoadingUI.Instance.EnableContainer("Loading World...");
        OnJoinLobbyWithIDStarted?.Invoke();
        try
        {
            joinedLobby = await LobbyService.Instance.JoinLobbyByIdAsync(lobbyId);
            string relayJoinCode = joinedLobby.Data[KEY_RELAY_JOIN_CODE].Value;
            Debug.Log($"Joining realy with code  {relayJoinCode}");

            JoinAllocation joinAllocation = await JoinRelay(relayJoinCode);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));

            MultiplayerManager.Instance.StartClient();
            OnJoinLobbyWithIDSuccess?.Invoke();
        }
        catch (LobbyServiceException e)
        {

            Debug.Log(e);
            OnJoinLobbyWithIDFailed?.Invoke(e.ToString());
        }
    }


    public async void LeaveLobby()
    {
        if (joinedLobby == null)
            return;
        try
        {
            //Ensure you sign-in before calling Authentication Instance
            //See IAuthenticationService interface
            string playerId = AuthenticationService.Instance.PlayerId;

            await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, playerId);

            if (joinedLobby != null)
                joinedLobby = null;

            OnLobbyLeft?.Invoke(playerId);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }

    }
 

    public async void KickPlayerFromLobby(string playerId) 
    {
        if (joinedLobby == null)
            return;
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, playerId);
            OnKickFromLobby?.Invoke(playerId);

        }

        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

 

    public async void DeleteLobby() 
    {

        if (joinedLobby != null)
        {
            try
            {
                await LobbyService.Instance.DeleteLobbyAsync(joinedLobby.Id);

                joinedLobby = null;
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }
    }

    #endregion


    #region Relay
    private async Task<Allocation> AllocateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(4);

            return allocation;
        }
        catch (RelayServiceException e)
        {
            Debug.LogError(e);

            throw;
        }
    }

    private async Task<string> GetRelayJoinCode(Allocation allocation)
    {
        try
        {
            string relayJoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            return relayJoinCode;
        }
        catch (RelayServiceException e)
        {
            Debug.LogError(e);
            throw;
        }
    }

    private async Task<JoinAllocation> JoinRelay(string joinCode)
    {
        JoinAllocation joinAllocation;
        try
        {
            joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
            return joinAllocation;
        }
        catch (RelayServiceException e)
        {
            Debug.LogError($"Relay get join code failed");
            throw;
        }

    }
    #endregion

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

    

    public bool IsLobbyHost()
    {
        if (joinedLobby != null)
            return AuthenticationService.Instance.PlayerId == joinedLobby.HostId;
        else
            return false;
    }

    private void HandleLobbyHeartBeat()
    {
        if (joinedLobby != null && IsLobbyHost())
        { 

                m_HearbeatTimer -= Time.deltaTime;
                if (m_HearbeatTimer < 0)
                {
                    float heartbeatTimerMax = 15;
                    m_HearbeatTimer = heartbeatTimerMax;

                    LobbyService.Instance.SendHeartbeatPingAsync(joinedLobby.Id);
                }

        }
    }
}

