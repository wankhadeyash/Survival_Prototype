using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct AvatarSelectionInfo
{
    public GameObject m_Prefab;
    public string m_ClassType;
}

public class AvatarSelection : MonoBehaviour
{
    [SerializeField] private GameObject m_AvatarStandPosition;
    public GameObject CharacterPosition => m_AvatarStandPosition;

    [SerializeField] TextMeshProUGUI m_AvatarClassTypeText;
    public TextMeshProUGUI AvatarClassTypeText => m_AvatarClassTypeText;

    [SerializeField] private List<AvatarSelectionInfo> m_AvatarSelectionList = new List<AvatarSelectionInfo>();
    public List<AvatarSelectionInfo> AvatarSelectionList => m_AvatarSelectionList;

    private List<GameObject> m_AvatarPrefabGOList = new List<GameObject>();
    public List<GameObject> AvatarPrefabGOList => m_AvatarPrefabGOList;

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
        foreach (AvatarSelectionInfo avatarInfo in m_AvatarSelectionList) 
        {
            m_AvatarPrefabGOList.Add(Instantiate(avatarInfo.m_Prefab, m_AvatarStandPosition.transform.position, m_AvatarStandPosition.transform.rotation, transform));
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
        if(m_CurrentAvatarPrefabGO)
            m_CurrentAvatarPrefabGO.SetActive(false);

        m_CurrentAvatarPrefabGO = m_AvatarPrefabGOList[index];

        m_CurrentAvatarPrefabGO.SetActive(true);

        m_AvatarClassTypeText.text = m_AvatarSelectionList[index].m_ClassType.ToString();
    }


}
