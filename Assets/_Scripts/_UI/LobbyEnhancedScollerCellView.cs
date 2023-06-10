using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EnhancedUI.EnhancedScroller;
using UnityEngine.UI;

public class LobbyEnhancedScollerCellView : EnhancedScrollerCellView
{
    public TextMeshProUGUI m_LobbyName;
    private string m_LobbyId;
    [SerializeField] Button m_JoinLobbyButton;
    public void SetData(LobbyInfoEnhancedScrollerData data)
    {
       m_LobbyName.text = data.lobbyName;
        m_LobbyId = data.lobbyId;
    }


    private void OnEnable()
    {
        m_JoinLobbyButton.onClick.AddListener(() => OnJoinButtonClicked());
    }

    private void OnDisable()
    {
        m_JoinLobbyButton.onClick.RemoveAllListeners();
    }

    private void OnJoinButtonClicked() 
    {
        LobbyManager.JoinWithId(m_LobbyId);
    }
}

