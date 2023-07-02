using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Netcode;
using StarterAssets;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private GameObject m_PlayerCameraRoot;
    private ThirdPersonController m_ThirdPersonController;
    private void OnEnable()
    {
       m_ThirdPersonController  =GetComponent<ThirdPersonController>();
        if (!m_ThirdPersonController.m_Multiplayer)
            StartCoroutine(Co_SetCameraFllowAndLookAt());
        DontDestroyOnLoad(gameObject);
    }

    public override void OnNetworkSpawn()
    {
       if (!IsOwner)
           return;
        StartCoroutine(Co_SetCameraFllowAndLookAt());
    }

    private IEnumerator Co_SetCameraFllowAndLookAt()
    {
        yield return new WaitForSeconds(2f);
        CameraController.Instance.SetCameraFollow(m_PlayerCameraRoot.transform);
        yield return new WaitForSeconds(1f);
        
    }

}

