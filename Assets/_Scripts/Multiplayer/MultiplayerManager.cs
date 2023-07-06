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



public class MultiplayerManager : SingletonBase<MultiplayerManager>
{
    public static Action OnUnityAutheticationSuccesfull;
    public static Action OnUnityAutheticationFailed;

    public static Action OnAllocateRelayFailed;
    public static Action OnJoinRelayFailed;

    public string m_RealyCode;
    

    private void Start()
    {
        InitializeUnityAuthentication();
    }


    private async void InitializeUnityAuthentication()
    {
        LoadingUI.Instance.Show("Connecting to servers");
        if (UnityServices.State != ServicesInitializationState.Initialized)
        {
            InitializationOptions initializationOptions = new InitializationOptions();
            //initializationOptions.SetProfile(UnityEngine.Random.Range(0, 10000).ToString());

            try
            {
                await UnityServices.InitializeAsync(initializationOptions);

                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                OnUnityAutheticationSuccesfull?.Invoke();
                Debug.Log($"Unity authentication succesfull");
                LoadingUI.Instance.Hide();
            }
            catch (Exception e)
            {
                OnUnityAutheticationFailed?.Invoke();
                LoadingUI.Instance.Show("Failed to connect to servers", 4);
            }
        }
    }

    public void StartHost() 
    {
        CustomSceneManager.Instance.LoadScene(SceneInfo.MainWorld.ToString(), () => { AllocateRelay(); });
        
    }

    public void StartClient(string relayCode)
    {
        CustomSceneManager.Instance.LoadScene(SceneInfo.MainWorld.ToString(), ()=> { JoinRelay(relayCode); });

    }


    private async void JoinRelay(string joinCode)
    {
        try
        {
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));
            NetworkManager.Singleton.StartClient();
            Debug.Log("Joined relay " + m_RealyCode);
        }
        catch (Exception e) 
        {
            Debug.Log(e);
            OnJoinRelayFailed?.Invoke();
            LoadingUI.Instance.Show($"Failed to join game. Try again", 2f);
            CustomSceneManager.Instance.LoadScene(SceneInfo.Startup);

        }

    }

    //Only called when Starting as host
    private async void AllocateRelay()
    {
        try
        {
            Allocation joinAllocation = await RelayService.Instance.CreateAllocationAsync(4);
            m_RealyCode = await RelayService.Instance.GetJoinCodeAsync(joinAllocation.AllocationId);

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));
            NetworkManager.Singleton.StartHost();
            Debug.Log("Create relay "  + m_RealyCode);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            OnAllocateRelayFailed?.Invoke();
            LoadingUI.Instance.Show($"Failed to create game. Try again", 2f);
            CustomSceneManager.Instance.LoadScene(SceneInfo.Startup);
        }
    }






}

