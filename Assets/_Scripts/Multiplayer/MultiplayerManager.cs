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
                StartCoroutine(LoadingUI.Instance.Show("Failed to connect to servers", 4));
;
            }
        }
    }
}

