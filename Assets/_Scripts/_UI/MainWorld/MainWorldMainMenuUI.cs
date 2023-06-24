using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MainWorldMainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject m_Container;

    [SerializeField] Button m_QuitButton;

    private void OnEnable()
    {
        m_QuitButton.onClick.AddListener(() => LobbyManager.LeaveLobby());
    }

    private void OnDisable()
    {
        m_QuitButton.onClick.RemoveAllListeners();

    }
    void Awake()
    {

    }

    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (GameManager.CurrentState != GameState.Paused)
                ShowContainer();
            else
                HideContainer();
        }
    }

    public void ShowContainer() 
    {
        GameManager.SetGameState(GameState.Paused);
        m_Container.SetActive(true);
    }

    public void HideContainer() 
    {
        GameManager.SetGameState(GameState.Unpause);
        m_Container.SetActive(false);


    }

}

