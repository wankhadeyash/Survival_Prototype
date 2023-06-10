using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public enum SceneInfo
{
    MainMenu,
    MainWorld
}
public class CustomSceneManager : SingletonBase<CustomSceneManager>
{
    public static void LoadScene(string sceneName, Action OnSceneLoaded = null)
    {
        Instance.LoadSceneInternal(sceneName, OnSceneLoaded);
    }

    private void LoadSceneInternal(string sceneName, Action OnSceneLoaded)
    {
        StartCoroutine(LoadSceneAsync(sceneName, OnSceneLoaded));
    }

    private System.Collections.IEnumerator LoadSceneAsync(string sceneName, Action OnSceneLoaded)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        OnSceneLoaded?.Invoke();
    }


    public static void LoadScene(int sceneIndex, Action OnSceneLoaded = null)
    {
        Instance.LoadSceneInternal(sceneIndex, OnSceneLoaded);
    }

    private void LoadSceneInternal(int sceneIndex, Action OnSceneLoaded)
    {
        StartCoroutine(LoadSceneAsync(sceneIndex, OnSceneLoaded));
    }

    private System.Collections.IEnumerator LoadSceneAsync(int sceneIndex, Action OnSceneLoaded)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        OnSceneLoaded?.Invoke();
    }
    public static void RestartScene(Action onSceneLoaded = null)
    {
        Instance.RestartSceneInternal(onSceneLoaded);
    }

    private void RestartSceneInternal(Action onSceneLoaded)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        LoadScene(currentScene.name, onSceneLoaded);
    }

    public static void LoadSceneOnNetwork(SceneInfo sceneToLoad)
    {
        
        NetworkManager.Singleton.SceneManager.LoadScene(sceneToLoad.ToString(), LoadSceneMode.Single);
    }

  
}
