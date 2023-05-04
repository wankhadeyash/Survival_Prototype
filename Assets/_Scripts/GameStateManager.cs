using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : SingletonBase<GameStateManager>
{
    // Define an enumeration to represent the different game states.
    public enum GameState { Playing, Paused, GameOver };

    // Define UnityEvents to be invoked when the state changes or is entered.
    public UnityEvent<GameState> onStateChanged;
    public UnityEvent<GameState> onStateEntered;

    // The current game state.
    private GameState currentState;

    // A static property to access the current game state from anywhere in the game.
    public static GameState CurrentState => Instance.currentState;

    // Set the initial game state to Playing when the game starts.
    private void Start()
    {
        SetState(GameState.Playing);
    }

    // A static method to change the game state from anywhere in the game.
    public static void SetState(GameState state)
    {
        Instance.SetStateInternal(state);
    }

    // A non-static method to change the game state.
    public void SetStateInternal(GameState state)
    {
        // Remember the previous state.
        GameState previousState = currentState;

        // Set the new state.
        currentState = state;

        // Take appropriate actions based on the new state.
        switch (currentState)
        {
            case GameState.Playing:
                // Resume time when playing.
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                // Pause time when paused or game over.
                Time.timeScale = 0f;
                // Show game over UI or do other game over-related logic
                break;
            default:
                break;
        }

        // Invoke the onStateChanged event with the new state.
        onStateChanged?.Invoke(currentState);

        // If the state has changed, invoke the onStateEntered event with the new state.
        if (previousState != currentState)
        {
            onStateEntered?.Invoke(currentState);
        }
    }
}
