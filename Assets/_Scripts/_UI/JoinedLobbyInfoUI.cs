using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class JoinedLobbyInfoUI : MonoBehaviour
{
    [SerializeField] private GameObject m_CreateLobbyUI;

    [SerializeField] private TextMeshProUGUI m_CurrentLobbyNameText;
    [SerializeField] private TextMeshProUGUI m_CurrentLobbyCodeText;
    [SerializeField] private Button m_LeaveCurrentLobbyButton;

    private void OnEnable()
    {
        m_LeaveCurrentLobbyButton.onClick.AddListener(() => OnLeaveLobbyButtonClicked());
        LobbyManager.OnLobbyLeft += OnLobbyLeft;

        SetCurrentLobbyData();

    }

    private void OnDisable()
    {
        m_LeaveCurrentLobbyButton.onClick.RemoveAllListeners();
        LobbyManager.OnLobbyLeft -= OnLobbyLeft;

        m_CurrentLobbyNameText.text = "";
        m_CurrentLobbyCodeText.text = "";

        m_LeaveCurrentLobbyButton.gameObject.SetActive(false);

    }

    private void OnLobbyLeft()
    {
        

        m_CreateLobbyUI.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnLeaveLobbyButtonClicked()
    {
        LobbyManager.LeaveLobby();
    }


    void SetCurrentLobbyData() 
    {
        if (LobbyManager.JoinedLobby != null) 
        {
            m_CurrentLobbyNameText.text = LobbyManager.JoinedLobby.Name;
            m_CurrentLobbyCodeText.text = LobbyManager.JoinedLobby.LobbyCode;

            m_LeaveCurrentLobbyButton.gameObject.SetActive(true);
        }

    }

}

