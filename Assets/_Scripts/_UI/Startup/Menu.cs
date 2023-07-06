using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Doozy;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.Reactor.Animators;
using System;

public class Menu : MonoBehaviour
{
    [SerializeField] UIButton m_MultiplayerButton;

    private void OnEnable()
    {
        m_MultiplayerButton.onClickEvent.AddListener(() => CustomSceneManager.Instance.LoadScene(SceneInfo.MainMenu));
        MultiplayerManager.OnUnityAutheticationFailed += MultiplayerManager_AuthenticationFailed;
    }

    private void OnDisable()
    {
        m_MultiplayerButton.onClickEvent.RemoveAllListeners();
        MultiplayerManager.OnUnityAutheticationFailed -= MultiplayerManager_AuthenticationFailed;

    }


    void Awake()
    {

    }

    void Start()
    {
        
    }

    public void EnableMultiplayerButton() 
    {
        
    }

    private void MultiplayerManager_AuthenticationFailed()
    {
        DisableMultiplayerButton();
    }
    public void DisableMultiplayerButton() 
    {
        m_MultiplayerButton.interactable = false;
        m_MultiplayerButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.grey;
    }
}
