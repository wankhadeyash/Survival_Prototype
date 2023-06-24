using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;



// Define an enumeration to represent the different game states.
public enum GameState 
{ 
    MainMenu,
    Start,
    Playing,
    Paused,
    Unpause,
    GameOver
};
public class GameManager : NetworkBehaviour
{
    [SerializeField] GameObject m_PlayerPrefab;

    public static Action OnMainMenuEntered;
    public static Action OnStartEntered;
    public static Action OnPlayingEntered;
    public static Action OnPausedEntered;
    public static Action OnUnPausedEntered;
    public static Action OnGameOverEntered;

    public static Action OnMainMenuExited;
    public static Action OnStartExited;
    public static Action OnPlayingExited;
    public static Action OnPausedExited;
    public static Action OnUnPausedExited;
    public static Action OnGameOverExited;


    public static GameManager Instance;

    // The current game state.
    private GameState currentState;

    // A static property to access the current game state from anywhere in the game.
    public static GameState CurrentState => Instance.currentState;

    private void Awake()
    {
        Instance = this;
    }

    // Set the initial game state to Playing when the game starts.
    private void Start()
    {
        SetGameState(GameState.MainMenu);
    }

    // A static method to change the game state from anywhere in the game.
    public static void SetGameState(GameState state)
    {
        Instance.SetStateInternal(state);
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;
            NetworkManager.Singleton.SceneManager.OnLoadComplete += SceneManager_OnLoadEventCompleted;
        }
    }

    private void SceneManager_OnLoadEventCompleted(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
    {
        GameObject playerTransform = Instantiate(m_PlayerPrefab);
        playerTransform.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
    }

    //private void SceneManager_OnLoadEventCompleted(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    //{
    //    foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
    //    {
    //        GameObject playerTransform = Instantiate(m_PlayerPrefab);
    //        playerTransform.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
    //    }
    //}

    private void NetworkManager_OnClientDisconnectCallback(ulong obj)
    {

    }

    // A non-static method to change the game state.
    private void SetStateInternal(GameState state)
    {
        // Remember the previous state.
        GameState previousState = currentState;

        // Set the new state.
        currentState = state;

        // Take appropriate actions based on the new state.
        switch (currentState)
        {

            case GameState.MainMenu:
                OnMainMenuEntered?.Invoke();
                // Pause time when paused or game over.
                // Show game over UI or do other game over-related logic
                break;
            case GameState.Start:
                OnStartEntered?.Invoke();
                // Pause time when paused or game over.
                // Show game over UI or do other game over-related logic
                break;
            case GameState.Playing:
                OnPlayingEntered?.Invoke();
                // Resume time when playing.
                break;
            case GameState.Paused:
                OnPausedEntered?.Invoke();
                break;
            case GameState.Unpause:
                OnUnPausedEntered?.Invoke();
                break;
            case GameState.GameOver:
                OnGameOverEntered?.Invoke();
                // Pause time when paused or game over.
                // Show game over UI or do other game over-related logic
                break;
           
            default:
                break;
        }

        ///Call exited state evenets
        switch (previousState)
        {
            case GameState.MainMenu:
                OnMainMenuExited?.Invoke();
                break;
            case GameState.Start:
                OnStartExited?.Invoke();
                break;
            case GameState.Playing:
                OnPlayingExited?.Invoke();
                break;
            case GameState.Paused:
                OnPausedExited?.Invoke();
                break;
            case GameState.Unpause:
                OnUnPausedExited?.Invoke();
                break;
            case GameState.GameOver:
                OnGameOverExited?.Invoke();
                break;
        }
    }
}
