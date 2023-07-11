using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerSerializedData: SaveLoadBase
{
    public string playerName;
    public int avatarIndex;

    public float health;

    public PlayerSerializedData(string folderName, string fileName) 
    {
        base.m_DirPath = folderName;
        base.m_FileName = fileName;
    }

}

