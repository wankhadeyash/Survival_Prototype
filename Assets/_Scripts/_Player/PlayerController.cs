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
    [SerializeField] private GameObject m_Geometry;
    public GameObject Geometry => m_Geometry;
    private ThirdPersonController m_ThirdPersonController;
    private Animator m_Animator;
    [SerializeField] private AvatarListSO m_AvatarsList;

    private void OnEnable()
    {
       m_ThirdPersonController  =GetComponent<ThirdPersonController>();
        m_Animator = GetComponent<Animator>();
        if (!m_ThirdPersonController.m_Multiplayer)
            StartCoroutine(Co_SetCameraFllowAndLookAt());
        DontDestroyOnLoad(gameObject);
    }

    public override void OnNetworkSpawn()
    {
       if (!IsOwner)
           return;

       // MultiplayerSpawnManager.Instance.SpawnAvatarOnDemand(PlayerDataManager.Instance.m_Data.avatarIndex);
        //SetAvatar(PlayerDataManager.Instance.m_Data.avatarIndex);
        StartCoroutine(Co_SetCameraFllowAndLookAt());
    }

    private IEnumerator Co_SetCameraFllowAndLookAt()
    {
        yield return new WaitForSeconds(2f);
        CameraController.Instance.SetCameraFollow(m_PlayerCameraRoot.transform);
        yield return new WaitForSeconds(1f);
        
    }

    private void SetAvatar(int index) 
    {
        AvatarData avatar = m_AvatarsList.avatars[index];
        m_Animator.avatar = avatar.animatorAvatar;
        if (m_Geometry.transform.childCount > 0) 
        {
           Destroy(m_Geometry.transform.GetChild(0).gameObject);
        }

        Instantiate(avatar.prefab, m_Geometry.transform.position,m_Geometry.transform.rotation,m_Geometry.transform);
        m_Animator.Rebind();

    }

}

