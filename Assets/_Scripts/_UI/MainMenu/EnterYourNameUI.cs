using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Doozy.Runtime.UIManager.Components;

public class EnterYourNameUI : MonoBehaviour
{
    [SerializeField] TMP_InputField m_NameIF;

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        Invoke(nameof(SetNameIFText),2);
    }

    public void OnConfirmButtonClicked() 
    {
        PlayerDataManager.Instance.SetPlayerName(m_NameIF.text);
    }

    void SetNameIFText() 
    {
        if (PlayerDataManager.Instance.m_Data != null)
        {
            m_NameIF.text = PlayerDataManager.Instance.m_Data.playerName;
        }
    }

}

