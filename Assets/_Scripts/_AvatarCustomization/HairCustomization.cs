using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairCustomization : MonoBehaviour
{
    [SerializeField] List<HairData> m_HairList = new List<HairData>();
    MeshFilter m_MeshFilter;
    MeshRenderer m_MeshRenderer;

    private void Awake()
    {
        m_MeshFilter = GetComponent<MeshFilter>();
        m_MeshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeHairMesh(int hairIndex, Action callback)
    {
        // Update the character's hair mesh based on the selected index
        m_MeshFilter.mesh = m_HairList[hairIndex].m_HairPrefab.GetComponent<MeshFilter>().sharedMesh;
        m_MeshRenderer.material = m_HairList[hairIndex].m_HairPrefab.GetComponent<MeshRenderer>().sharedMaterial;
        callback?.Invoke();
    }

    public void LoadHairMesh(int hairIndex) 
    {
        ChangeHairMesh(hairIndex, () => { });
    }
}
