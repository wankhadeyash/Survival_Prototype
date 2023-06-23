using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LobbyCodeUI : MonoBehaviour
{

    [SerializeField] GameObject m_Container;

    [SerializeField] TextMeshProUGUI m_LobbyCodeText;
    [SerializeField] private float m_LobbyCodeFetchInterval;
    void Awake()
    {

    }

    void Start()
    {
        SetLobbyCode();
        StartCoroutine(Co_FetchLobbyCoded());
    }

    IEnumerator Co_FetchLobbyCoded() 
    {
        while (true)
        {
            yield return new WaitForSeconds(m_LobbyCodeFetchInterval);
            SetLobbyCode();
        }
    }

    void SetLobbyCode() 
    {
        if (LobbyManager.Instance == null || LobbyManager.JoinedLobby == null) 
        {
            m_LobbyCodeText.text = "Not Connected";
            return;
        }
        m_LobbyCodeText.text = LobbyManager.JoinedLobby.LobbyCode.ToString();
        
    }

}

