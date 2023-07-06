using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Doozy.Runtime.UIManager.Components;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] UIButton m_CreateBT;

    [SerializeField] TMP_InputField m_RelayCodeIF;
    [SerializeField] UIButton m_JoinBT;

    void OnEnable()
    {
        m_CreateBT.onClickEvent.AddListener(()=>
        {
            MultiplayerManager.Instance.StartHost();

        });

        m_JoinBT.onClickEvent.AddListener(()=>
        {
            MultiplayerManager.Instance.StartClient(m_RelayCodeIF.text);

        });

    }

    void OnDisable() {
        m_CreateBT.onClickEvent.RemoveAllListeners();
        m_JoinBT.onClickEvent.RemoveAllListeners();
    }
    void Awake()
    {
            
    }

    void Start()
    {
            
    }     

}

