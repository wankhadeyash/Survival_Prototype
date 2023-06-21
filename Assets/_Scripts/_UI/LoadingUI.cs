using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LoadingUI : MonoBehaviour
{
    public static LoadingUI Instance;

    [SerializeField] private GameObject m_Container;
    [SerializeField] private TextMeshProUGUI m_Text;


    private void OnEnable()
    {
        Instance = this;
    }


    void Awake()
    {
        DisableContainer();
    }

    public void SetLoadingText() 
    {
        
    }
    public void EnableContainer(string text) 
    {
        m_Text.text = text;
        m_Container.SetActive(true);
    }

    public void DisableContainer() 
    {
        m_Text.text = "";
        m_Container.SetActive(false);
    }

}

