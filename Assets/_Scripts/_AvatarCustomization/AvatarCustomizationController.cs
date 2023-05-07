using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class AvatarData:SaveLoadDataBase
{
    public int hairIndex;
    
    public AvatarData(string folderName, string fileName)
    {
        base.m_DirPath = folderName;
        base.m_FileName = fileName;
    }

    
}
public class AvatarCustomizationController : MonoBehaviour
{
    [SerializeField] HairCustomization m_HairCustomization;
    AvatarData m_AvatarData = new AvatarData("Player", "AvatarData");
    // Start is called before the first frame update
    void Start()
    {
        LoadAvatarCustomization();   
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //    ChangeHair(0);
        //if (Input.GetKeyDown(KeyCode.S)) 
        //{
        //    SaveAvatarCustomization();
        //}

    }

    void ChangeHair(int hairIndex) 
    {
        m_HairCustomization.ChangeHairMesh(hairIndex, ()=> { m_AvatarData.hairIndex = hairIndex; });
    }

    public void SaveAvatarCustomization()
    {
        // Save the user's avatar customization choices
        //Serializer.SaveBinaryData(m_AvatarData);
        Serializer.SaveJsonData(m_AvatarData);
    }

    public void LoadAvatarCustomization()
    {
        // Load the user's saved avatar customization choices
        // AvatarData data = Serializer.LoadBinaryData(m_AvatarData);
        AvatarData data = Serializer.LoadJsonData(m_AvatarData);
        if (data != null)
        {
            m_HairCustomization.LoadHairMesh(data.hairIndex);
        }
    }

}
