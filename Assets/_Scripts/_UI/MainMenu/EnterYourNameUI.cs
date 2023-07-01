using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Doozy.Runtime.UIManager.Components;

public class EnterYourNameUI : MonoBehaviour
{
    [SerializeField] TMP_InputField m_NameIF;
    [SerializeField] UIButton m_ConfirmBT;
    private void OnEnable()
    {
        m_ConfirmBT.onClickEvent.AddListener(() => OnConfirmButtonClicked());
    }

    private void OnDisable()
    {
        m_ConfirmBT.onClickEvent.RemoveAllListeners();
    }
    private void Start()
    {
        Invoke(nameof(SetNameIFText),0.5f);
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

