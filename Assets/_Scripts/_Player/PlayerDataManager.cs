using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : SingletonBase<PlayerDataManager>
{
    [SerializeField] private AvatarListSO m_AvatarList;
    public AvatarListSO AvatarList => m_AvatarList;
    [HideInInspector] public PlayerSerializedData m_Data;

    public void Start()
    {
        m_Data = new PlayerSerializedData("PlayerData", "SavedData");
        if (Serializer.DoesFileExists(m_Data)) 
        {
            m_Data = Serializer.LoadJsonData(m_Data);
        }
        StartCoroutine(LoadingUI.Instance.Hide(2f));

    }

    public void SetPlayerName(string name) 
    {
        Debug.Log("Saving player name");
        m_Data.playerName = name;
        Serializer.SaveJsonData(m_Data);
    }

    public void SetAvatarIndex(int index) 
    {
        m_Data.avatarIndex = index;
        Serializer.SaveJsonData(m_Data);
    }
}

