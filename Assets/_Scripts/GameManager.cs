using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using System;


// Define an enumeration to represent the different game states.
public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    Unpause,
    GameOver
};
public class GameManager : SingletonBase<GameManager>
{
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

    // The current game state.
    private GameState currentState;

    // A static property to access the current game state from anywhere in the game.
    public static GameState CurrentState => Instance.currentState;

 

    private void Start()
    {
        SetGameState(GameState.MainMenu);
    }

    // A static method to change the game state from anywhere in the game.
    public static void SetGameState(GameState state)
    {
        Instance.SetStateInternal(state);
    }

    // A non-static method to change the game state.
    private void SetStateInternal(GameState state)
    {
        // Remember the previous state.
        GameState previousState = currentState;

        // Set the new state.
        currentState = state;

        ///Call exited state evenets
        switch (previousState)
        {
            case GameState.MainMenu:
                OnMainMenuExited?.Invoke();
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

        // Take appropriate actions based on the new state.
        switch (currentState)
        {

            case GameState.MainMenu:
                OnMainMenuEntered?.Invoke();
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
                SetGameState(GameState.Playing);
                break;
            case GameState.GameOver:
                OnGameOverEntered?.Invoke();
                // Pause time when paused or game over.
                // Show game over UI or do other game over-related logic
                break;

            default:
                break;
        }

       
    }
}

