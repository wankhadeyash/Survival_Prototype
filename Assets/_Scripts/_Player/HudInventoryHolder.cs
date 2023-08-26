using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlankBrains.Inventory;
public class HudInventoryHolder : InventoryHolder
{
    InventorySlot m_CurrentEquipedSlot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
            return;
        base.OnNetworkSpawn();
        FindObjectOfType<HudInventoryDisplay>().SetInventoryHolder(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
            return;
        SwitchItem();
    }

    void SwitchItem() 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (InventoryManager.InventorySlots[0].ItemData != null)
            {
                m_CurrentEquipedSlot?.UnEquipeSlot();
                InventorySlot slotToEquipe = InventoryManager.InventorySlots[0];
                InventoryManager.EquipeSlot(slotToEquipe);
                m_CurrentEquipedSlot = slotToEquipe;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (InventoryManager.InventorySlots[1].ItemData != null)
            {
                m_CurrentEquipedSlot?.UnEquipeSlot();
                InventorySlot slotToEquipe = InventoryManager.InventorySlots[1];
                InventoryManager.EquipeSlot(slotToEquipe);
                m_CurrentEquipedSlot = slotToEquipe;
            }
        }
    }
}
