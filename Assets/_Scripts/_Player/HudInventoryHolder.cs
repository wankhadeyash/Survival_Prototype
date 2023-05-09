using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudInventoryHolder : InventoryHolder
{
    [SerializeField] Transform m_HandPos;
    ItemController m_CurrentEquipedItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SwitchItem();
    }

    void SwitchItem() 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_CurrentEquipedItem?.OnUnequipped();
            GameObject item = Instantiate(InventoryManager.InventorySlots[0].ItemData.itemControllerPrefab,m_HandPos.transform.position,Quaternion.identity,m_HandPos);
            item.GetComponent<ItemController>().OnEquipped();
        }
    }
}
