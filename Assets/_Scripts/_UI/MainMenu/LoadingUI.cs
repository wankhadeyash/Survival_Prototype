using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LoadingUI : SingletonBase<LoadingUI>
{

    [SerializeField] private GameObject m_Container;
    [SerializeField] private TextMeshProUGUI m_Text;


    protected override void OnAwake()
    {
        DisableContainer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            EnableContainer("yash");
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

