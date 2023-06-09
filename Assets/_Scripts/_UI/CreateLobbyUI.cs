using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.Services.Lobbies.Models;

public class CreateLobbyUI : MonoBehaviour
{
    [SerializeField] private GameObject m_JoinedLobbyInfoUI;

    [Header("Create Lobby")]
    [SerializeField] private TMP_InputField m_LobbyNameIF;
    [SerializeField] private Toggle m_LobbyTypeToggle;

    //[SerializeField] private Button m_CreateLobbyButton;
    //[SerializeField] private Button m_QuickJoinLobbyButton;
    //[SerializeField] private Button m_StartGameButton;
    [SerializeField] private Button m_CloseButton;
    [SerializeField] private Button m_CreateButton;

    

    [Header("Join Lobby with Code")]
    [SerializeField] private TMP_InputField m_LobbyCodeIF;
    [SerializeField] private Button m_JoinLobbyWithCodeButton;
    [SerializeField] private TextMeshProUGUI m_LobbyCodeError;


    public GameObject m_CreateGameUIObject;
    public GameObject m_NavigationUIObject;

    void Awake()
    {
  
    }

    private void OnEnable()
    {
        LobbyManager.OnLobbyJoined += OnLobbyJoined;
        //m_CreateLobbyButton.onClick.AddListener(() => OnCreateLobbyButtonClicked());
        //m_StartGameButton.onClick.AddListener(() => OnStartGameButtonClicked());
        //m_QuickJoinLobbyButton.onClick.AddListener(() => OnQuickJoinLobbyButtonClicked());

        m_CreateButton.onClick.AddListener(() => OnCreateLobbyButtonClicked());
        m_CloseButton.onClick.AddListener(() => OnCloseButtonClicked());

        m_JoinLobbyWithCodeButton.onClick.AddListener(() => OnJoinLobbyWithCodeButtonClicked());

        m_LobbyCodeError.text = "";

    }

    

    private void OnDisable()
    {
        LobbyManager.OnLobbyJoined -= OnLobbyJoined;

        //m_CreateLobbyButton.onClick.RemoveAllListeners();
        //m_StartGameButton.onClick.RemoveAllListeners();
        //m_QuickJoinLobbyButton.onClick.RemoveAllListeners();


        m_CloseButton.onClick.RemoveAllListeners();
        m_CreateButton.onClick.RemoveAllListeners();
        m_JoinLobbyWithCodeButton.onClick.RemoveAllListeners();
    }


    private void OnLobbyJoined(ClientType clientType, Lobby joinedLobby)
    {
        m_JoinedLobbyInfoUI.SetActive(true);
    }

    void Start()
    {

    }

    public void OnCreateLobbyButtonClicked() 
    {
        LobbyManager.CreateLobby(m_LobbyNameIF.text, m_LobbyTypeToggle.isOn);
    }

    public void OnQuickJoinLobbyButtonClicked() 
    {
        LobbyManager.QuickJoin();
    }

    public void OnStartGameButtonClicked() 
    {
        CustomSceneManager.LoadScene(1,()=> { MultiplayerManager.JoinHost(); });
    }

    public void OnCloseButtonClicked() 
    {
        m_CreateGameUIObject.SetActive(false);
        m_NavigationUIObject.SetActive(true);
    }



    public async void OnJoinLobbyWithCodeButtonClicked() 
    {
        if (string.IsNullOrEmpty(m_LobbyCodeIF.text))
            return;

        string joinResult = await LobbyManager.JoinWithCode(m_LobbyCodeIF.text);
        m_LobbyCodeError.text = joinResult;
        StartCoroutine(Co_ClearTextField(m_LobbyCodeError, 2));
        
    }

    private IEnumerator Co_ClearTextField(TextMeshProUGUI textFieldToClear, float timeAfterClear) 
    {
        yield return new WaitForSeconds(timeAfterClear);
        textFieldToClear.text = "";

    }
}

