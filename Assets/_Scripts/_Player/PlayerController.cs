using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Netcode;
using StarterAssets;
using UnityEngine.Events;
public class PlayerController : NetworkBehaviour
{
    [SerializeField] private GameObject m_PlayerCameraRoot;
    private ThirdPersonController m_ThirdPersonController;
    private Animator m_Animator;

    //Animations Ids
    private int _animIDPunch;

    private void OnEnable()
    {
        m_ThirdPersonController = GetComponent<ThirdPersonController>();
        if (!m_ThirdPersonController.m_Multiplayer)
            SetStartingProperties();
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
            return;
        SetStartingProperties();
        // MultiplayerSpawnManager.Instance.SpawnAvatarOnDemand(PlayerDataManager.Instance.m_Data.avatarIndex);
        //SetAvatar(PlayerDataManager.Instance.m_Data.avatarIndex);
    }

    void SetStartingProperties() 
    {
        m_Animator = GetComponent<Animator>();
        StartCoroutine(Co_SetCameraFllowAndLookAt());
        SetAnimationsIds();
        
    }

    void SetAnimationsIds()
    {
        _animIDPunch = Animator.StringToHash("Punch");
    }

    private void Update()
    {
        if (!IsOwner)
            return;
        if (Input.GetMouseButtonDown(0)) 
        {
            m_Animator.SetTrigger(_animIDPunch);
        }
    }

    private IEnumerator Co_SetCameraFllowAndLookAt()
    {
        yield return new WaitForSeconds(2f);
        CameraController.Instance.SetCameraFollow(m_PlayerCameraRoot.transform);
        yield return new WaitForSeconds(1f);
    }

    //Called from animation events
    public void StartAttack() 
    {
        m_ThirdPersonController.m_CanMove = false;

    }

    //Called from animation events

    public void StopAttack() 
    {
        m_ThirdPersonController.m_CanMove = true;

    }

    public void ShakeCamera() 
    {
        CameraController.Instance.StartCameraShake(0.5f, 0.25f);
    }



}

