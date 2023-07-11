using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlankBrains.Inventory
{
    [System.Serializable]
    public class InventorySlot
    {
        [SerializeField] InventoryItemData m_ItemData; // The data of the item that is stored in this slot
        public InventoryItemData ItemData => m_ItemData;

        [SerializeField] GameObject m_ItemController;// Storing the item controller associated with this slot. Used when equipping item
        public GameObject ItemController => m_ItemController;

        [SerializeField] GameObject m_ItemPickUp; // Storing item pickup associated with this slot. Used when dropping an item
        public GameObject ItemPickUp => m_ItemPickUp;

        [SerializeField] int m_StackSize; // The amount of items stored in this slot
        public int StackSize => m_StackSize;

        Transform m_EquipeItemPos, m_DropItemPos;
        // Constructor that initializes the slot with an item and a stack size
        public InventorySlot(InventoryItemData itemToAdd, int amount, Transform equipeItemPosition, Transform dropItemPosition)
        {
            m_ItemData = itemToAdd;
            m_StackSize = amount;

            m_EquipeItemPos = equipeItemPosition;
            m_DropItemPos = dropItemPosition;
        }

        // Default constructor that initializes the slot with no item and a stack size of -1
        public InventorySlot(Transform equipeItemPosition, Transform dropItemPosition)
        {
            ClearSlot();

            m_EquipeItemPos = equipeItemPosition;
            m_DropItemPos = dropItemPosition;
        }

        // Clears the slot by setting the item data to null and the stack size to -1
        //Clear InventorySlot_UI first, beacause that function will call this method internally, ensuring to clear both UI slot and item slot 
        public void ClearSlot()
        {
            if (m_ItemController != null)
                GameObject.Destroy(m_ItemController.gameObject);

            //if (m_ItemPickUp != null)
            //    GameObject.Destroy(m_ItemPickUp.gameObject);

            m_ItemController = null;
            m_ItemPickUp = null;

            m_ItemData = null;
            m_StackSize = -1;
        }

        // Updates the slot with new item data and stack size
        public void UpdateSlot(InventoryItemData item, int amount)
        {
            m_ItemData = item;
            m_StackSize = amount;

            //Check if Itemcontroller is empty
            //If not destroy the gameobject to avoid duplicates
            if (m_ItemController != null)
                GameObject.Destroy(m_ItemController.gameObject);

            m_ItemController = GameObject.Instantiate(m_ItemData.itemControllerPrefab, m_EquipeItemPos.position, Quaternion.identity, m_EquipeItemPos);
            m_ItemController.SetActive(false);

            //Check if ItemPickUp is empty
            //If not destroy the gameobject to avoid duplicates
            if (m_ItemPickUp != null)
                GameObject.Destroy(m_ItemPickUp.gameObject);

            m_ItemPickUp = GameObject.Instantiate(m_ItemData.itemPickUpPrefab, m_DropItemPos.position, Quaternion.identity, m_DropItemPos);
            m_ItemPickUp.SetActive(false);


        }

        public void EquipeSlot()
        {
            m_ItemController.SetActive(true);
            m_ItemController.GetComponent<ItemController>().OnEquipped();
        }

        public void UnEquipeSlot()
        {
            //Means there is nothing to unequip
            if (m_ItemData == null)
                return;

            m_ItemController.GetComponent<ItemController>().OnUnequipped();

            m_ItemController.SetActive(false);

        }

        public void DropItem()
        {
            m_ItemPickUp.SetActive(true);
            m_ItemPickUp.GetComponent<ItemPickUp>().m_StackSize = m_StackSize;
            m_ItemPickUp.transform.SetParent(null);


            m_ItemController.GetComponent<ItemController>().OnItemDroppedFromInventory();
            GameObject.Destroy(m_ItemController.gameObject);
        }

        // Checks if there is enough room in the stack to add more items
        bool RoomLeftInStack(int amountToAdd)
        {
            if (m_StackSize + amountToAdd <= m_ItemData.maxQuantity)
                return true;
            return false;
        }

        // Adds the specified amount of items to the stack if there is enough room
        public void AddToStack(int amountToadd)
        {
            if (RoomLeftInStack(amountToadd))
            {
                m_StackSize += amountToadd;
            }
        }

        // Removes the specified amount of items from the stack
        public void RemoveFromStack(int amountToRemove)
        {
            m_StackSize -= amountToRemove;
        }
    }
}
