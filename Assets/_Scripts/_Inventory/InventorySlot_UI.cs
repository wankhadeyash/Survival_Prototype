using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot_UI : MonoBehaviour
{
    public Image m_Image;
    [SerializeField] InventorySlot m_AssignedInventorySlot;
    public InventorySlot AssignedInventorySlot => m_AssignedInventorySlot;

    private void Awake()
    {
        ClearSlot();
    }

    public void Init(InventorySlot slot) 
    {
        m_AssignedInventorySlot = slot;
    }

    public void UpdateUISlot(InventorySlot slot) 
    {
        m_AssignedInventorySlot = slot;
        m_Image.sprite = slot.m_ItemData.icon;
    }

    public void UpdateUISlot() 
    {
        if (m_AssignedInventorySlot != null) UpdateUISlot(m_AssignedInventorySlot);
    }

    public void ClearSlot() 
    {
        m_AssignedInventorySlot?.ClearSlot();
    }
}
