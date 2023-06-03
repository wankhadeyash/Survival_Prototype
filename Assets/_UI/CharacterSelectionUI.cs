using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class CharacterSelectionUI : MonoBehaviour
{
    public CinemachineVirtualCamera m_MainMenuCamera;
    public GameObject m_CanvasToDeactivate;

    void Awake()
    {

    }

    void Start()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            NetworkManager.Singleton.StartClient();

    }
    public void OnBackButtonClicked()
    {
        m_CanvasToDeactivate.SetActive(false);
        CinemachineCameraSwitcher.ActivateCamera(m_MainMenuCamera);

    }

    public void OnConfirmButtonClicked() 
    {
        CustomSceneManager.LoadScene(1, () => { NetworkManager.Singleton.StartHost(); });
    }
}

