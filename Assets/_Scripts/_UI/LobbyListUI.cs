using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using System;

public class LobbyListUI : MonoBehaviour
{

    private LobbyEnchancedScrollerController m_EnhancedScroller;
    float m_FetchLobbyTimer;
    void Awake()
    {
        m_EnhancedScroller = GetComponentInChildren<LobbyEnchancedScrollerController>();
    }

    private void OnEnable()
    {
    }


    private void OnDisable()
    {

    }


    void Start()
    {
    }
    private void Update()
    {
        GetLobbyList();


    }
    async void GetLobbyList()
    {
        if (LobbyManager.Instance.m_IsAuthenticated)
        {
            m_FetchLobbyTimer -= Time.deltaTime;
            if (m_FetchLobbyTimer < 0 && m_EnhancedScroller != null)
            {
                float fetchLobbyTimer = 5;
                m_FetchLobbyTimer = fetchLobbyTimer;

                m_EnhancedScroller.Data.Clear();
                List<Lobby> lobbyList = await LobbyManager.Instance.GetLobbiesList();
                foreach (Lobby lobby in lobbyList)
                {
                    m_EnhancedScroller.Data.Add(new LobbyInfoEnhancedScrollerData { lobbyName = lobby.Name, lobbyId = lobby.Id });
                }
                m_EnhancedScroller.myScroller.ReloadData();
            }
        }

    }


}

