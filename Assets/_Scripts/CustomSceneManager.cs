using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public enum SceneInfo
{
    Startup,
    MainMenu,
    MainWorld
}
public class CustomSceneManager : SingletonBase<CustomSceneManager>
{
    public static Action OnLoadSceneStarted;

    public static Action OnLoadSceneFinished;

    private void OnEnable()
    {
        
    }

    

    private void OnDisable()
    {
        

    }

    private void OnNetworkManager_Shutdown()
    {
        LoadScene(SceneInfo.MainMenu);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    public  void LoadScene(SceneInfo sceneInfo, Action OnSceneLoaded = null)
    {
        StartCoroutine(LoadSceneAsync(sceneInfo.ToString(), OnSceneLoaded));
    }

    private System.Collections.IEnumerator LoadSceneAsync(string sceneName, Action OnSceneLoaded = null)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        OnSceneLoaded?.Invoke();
        LoadingUI.Instance.Hide();
    }


    public  void LoadScene(int sceneIndex, Action OnSceneLoaded = null)
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
    public void RestartScene(Action onSceneLoaded = null)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        //LoadScene(currentScene.name, onSceneLoaded);  
    }



    public void LoadSceneOnNetwork(SceneInfo sceneToLoad, Action callback = null)
    {
        OnLoadSceneStarted?.Invoke();
        NetworkManager.Singleton.SceneManager.LoadScene(sceneToLoad.ToString(), LoadSceneMode.Single);
        NetworkManager.Singleton.SceneManager.OnLoadComplete -= OnLoadLevelComplete;
        NetworkManager.Singleton.SceneManager.OnLoadComplete += OnLoadLevelComplete;
    }

    private void OnLoadLevelComplete(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
    {
        OnLoadSceneFinished?.Invoke();
    }
}
