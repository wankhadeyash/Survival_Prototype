using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Doozy.Runtime.UIManager.Components;
using System;

public class CharacterSelectionUI : MonoBehaviour
{
    [SerializeField] UIButton m_ConfirmBT;
    public static Action OnCharacterConfirmed; 

    private void OnEnable()
    {
        m_ConfirmBT.onClickEvent.AddListener(() =>
        {
            OnCharacterConfirmed?.Invoke();
        });
    }

}

