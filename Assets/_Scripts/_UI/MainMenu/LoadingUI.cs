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
    [SerializeField] private Button m_BackButton;


    private void OnEnable()
    {
        m_BackButton.onClick.AddListener(() =>
        {
            MultiplayerManager.Instance.Disconnect();
        });

        StartCoroutine(Co_EnableBackButton());
    }

    private void OnDisable()
    {
        m_BackButton.onClick.RemoveAllListeners();
    }
    protected override void OnAwake()
    {
        DisableContainer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            EnableContainer("yash");
    }

    IEnumerator Co_EnableBackButton() 
    {
        m_BackButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(5);
        m_BackButton.gameObject.SetActive(true);
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

