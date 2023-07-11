using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class AvatarSelection : MonoBehaviour
{
    [SerializeField] private GameObject m_AvatarStandPosition;
    public GameObject CharacterPosition => m_AvatarStandPosition;

    [SerializeField] private AvatarListSO m_AvatarSelectionList;
    public AvatarListSO AvatarSelectionList => m_AvatarSelectionList;

    private List<GameObject> m_AvatarPrefabGOList = new List<GameObject>();
    public List<GameObject> AvatarPrefabGOList => m_AvatarPrefabGOList;

    public AvatarData m_CurrentAvatarData;

    private GameObject m_CurrentAvatarPrefabGO;
    public GameObject CurrentAvatarPrefabGO => m_CurrentAvatarPrefabGO;

    [SerializeField] private int m_CurrrentSelectedAvatarIndex;
    public int CurrentSelectedAvatarIndex => m_CurrrentSelectedAvatarIndex;

    [Header("Stats View")]
    [SerializeField] private List<StatsView> m_StatsView;

    [Header("Item View")]
    [SerializeField] private List<ItemView> m_ItemView;


    private void OnEnable()
    {
        CharacterSelectionUI.OnCharacterConfirmed += CharacterSelection_CharacterConfirmed;
    }
    private void OnDisable()
    {
        CharacterSelectionUI.OnCharacterConfirmed -= CharacterSelection_CharacterConfirmed;

    }
    private void CharacterSelection_CharacterConfirmed()
    {
        PlayerDataManager.Instance.SetAvatarIndex(m_CurrrentSelectedAvatarIndex);
    }

    private void Start()
    {
        InitializeAvatars();
    }

    void InitializeAvatars() 
    {
        int i = 0;
        foreach (AvatarData avatarData in m_AvatarSelectionList.avatars) 
        {
            m_AvatarPrefabGOList.Add(Instantiate(avatarData.avatarPrefab, m_AvatarStandPosition.transform.position, avatarData.avatarPrefab.transform.rotation, m_AvatarStandPosition.transform));
            m_AvatarPrefabGOList[i].SetActive(false);
            i++;
        }

        m_CurrrentSelectedAvatarIndex = PlayerDataManager.Instance.m_Data.avatarIndex;
        DisplayAvatar(m_CurrrentSelectedAvatarIndex);
    }

    public void CycleAvatar(int counter) 
    {
        m_CurrrentSelectedAvatarIndex += counter;

        if (m_CurrrentSelectedAvatarIndex < 0) 
        {
            m_CurrrentSelectedAvatarIndex = 0;
            return;
        }

        if (m_CurrrentSelectedAvatarIndex >= m_AvatarSelectionList.avatars.Count) 
        {
            m_CurrrentSelectedAvatarIndex = m_AvatarSelectionList.avatars.Count - 1;
            return;
        }

        DisplayAvatar(m_CurrrentSelectedAvatarIndex);
    }

    private void DisplayAvatar(int index) 
    {
        m_CurrentAvatarData = m_AvatarSelectionList.avatars[m_CurrrentSelectedAvatarIndex];

        if(m_CurrentAvatarPrefabGO)
            m_CurrentAvatarPrefabGO.SetActive(false);

        m_CurrentAvatarPrefabGO = m_AvatarPrefabGOList[index];

        m_CurrentAvatarPrefabGO.SetActive(true);

        UpdateStats();
        UpdateItemData();
    }

    private void UpdateStats() 
    {
        PlayerStatsSO stats = m_CurrentAvatarData.stats;
        m_StatsView[0].SetData(stats.startingHealth.GetBaseValue(), "Health");
        m_StatsView[1].SetData(stats.stamina.GetBaseValue(), "Stamina");
        m_StatsView[2].SetData(stats.armor.GetBaseValue(), "Health");


    }

    private void UpdateItemData() 
    {
        for (int i = 0; i < m_ItemView.Count; i++) 
        {
            if (i < m_CurrentAvatarData.m_DefaultItems.Count)
            {
                m_ItemView[i].gameObject.SetActive(true);
                m_ItemView[i].SetData(m_CurrentAvatarData.m_DefaultItems[i].data);
            }
            else 
            {
                m_ItemView[i].gameObject.SetActive(false);
            }
        }
    }

    
}
