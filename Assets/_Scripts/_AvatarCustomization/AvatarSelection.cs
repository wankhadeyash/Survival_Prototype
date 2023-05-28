using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class AvatarSelection : MonoBehaviour
{
    [SerializeField] private GameObject m_AvatarStandPosition;
    public GameObject CharacterPosition => m_AvatarStandPosition;

    [SerializeField] TextMeshProUGUI m_AvatarClassTypeText;
    public TextMeshProUGUI AvatarClassTypeText => m_AvatarClassTypeText;

    public Slider m_HealthSlider;

    public Slider m_ArmorSlider;

    public Slider m_StaminaSlider;

    public List<Image> m_DefaultItemsImageList = new List<Image>();








    [SerializeField] private List<AvatarData> m_AvatarSelectionList = new List<AvatarData>();
    public List<AvatarData> AvatarSelectionList => m_AvatarSelectionList;

    private List<GameObject> m_AvatarPrefabGOList = new List<GameObject>();
    public List<GameObject> AvatarPrefabGOList => m_AvatarPrefabGOList;

    public AvatarData m_CurrentAvatarData;

    private GameObject m_CurrentAvatarPrefabGO;
    public GameObject CurrentAvatarPrefabGO => m_CurrentAvatarPrefabGO;

    [SerializeField] private int m_CurrrentSelectedAvatarIndex;
    public int CurrentSelectedAvatarIndex => m_CurrrentSelectedAvatarIndex;


    private void Start()
    {
        InitializeAvatars();
    }

    void InitializeAvatars() 
    {
        int i = 0;
        foreach (AvatarData avatarData in m_AvatarSelectionList) 
        {
            m_AvatarPrefabGOList.Add(Instantiate(avatarData.prefab, m_AvatarStandPosition.transform.position, m_AvatarStandPosition.transform.rotation, m_AvatarStandPosition.transform));
            m_AvatarPrefabGOList[i].SetActive(false);
            i++;
        }

        DisplayAvatar(0);
    }

    public void CycleAvatar(int counter) 
    {
        m_CurrrentSelectedAvatarIndex += counter;

        if (m_CurrrentSelectedAvatarIndex < 0) 
        {
            m_CurrrentSelectedAvatarIndex = 0;
            return;
        }

        if (m_CurrrentSelectedAvatarIndex >= m_AvatarSelectionList.Count) 
        {
            m_CurrrentSelectedAvatarIndex = m_AvatarSelectionList.Count - 1;
            return;
        }

        DisplayAvatar(m_CurrrentSelectedAvatarIndex);

    }

    private void DisplayAvatar(int index) 
    {
        m_CurrentAvatarData = m_AvatarSelectionList[m_CurrrentSelectedAvatarIndex];

        if(m_CurrentAvatarPrefabGO)
            m_CurrentAvatarPrefabGO.SetActive(false);

        m_CurrentAvatarPrefabGO = m_AvatarPrefabGOList[index];

        m_CurrentAvatarPrefabGO.SetActive(true);

        m_AvatarClassTypeText.text = m_AvatarSelectionList[index].classType.ToString();

        DisplayStats();
        DisplayDefaultItems();
    }

    private void DisplayStats() 
    {
        m_HealthSlider.value = m_CurrentAvatarData.health;
        m_ArmorSlider.value = m_CurrentAvatarData.armor;
        m_StaminaSlider.value = m_CurrentAvatarData.stamina;
    }

    private void DisplayDefaultItems() 
    {
        for (int i = 0; i < m_DefaultItemsImageList.Count; i++) 
        {
            if (i >= m_CurrentAvatarData.m_DefaultItems.Count)
                m_DefaultItemsImageList[i].gameObject.SetActive(false);
            else
            {
                m_DefaultItemsImageList[i].gameObject.SetActive(true);
                m_DefaultItemsImageList[i].sprite = m_CurrentAvatarData.m_DefaultItems[i].icon;
            }
        }
    }


}
