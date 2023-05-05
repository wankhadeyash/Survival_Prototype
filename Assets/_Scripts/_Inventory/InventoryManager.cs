using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // The maximum number of slots available in the inventory
    public int m_MaxSlots = 20;

    // A list to hold the items in the inventory
    public List<Item> m_Items = new List<Item>();

    public List<ItemController> m_ItemControllers = new List<ItemController>();

    // Add an item to the inventory
    public void AddItem(ItemController itemController)
    {
        // Check if there is space in the inventory
        if (m_Items.Count >= m_MaxSlots)
        {
            Debug.Log("Inventory is full!");
            return;
        }
        // Add the item to the inventory
        m_Items.Add(itemController.item);

        //Add th itemController
        m_ItemControllers.Add(itemController);

        //Callback when Item is added to inventory
        itemController.OnAddedToInventory();
    }

    // Remove an item from the inventory
    public void RemoveItem(ItemController itemController)
    {
        // Check if the item is in the inventory
        if (!m_ItemControllers.Contains(itemController))
        {
            Debug.Log("Item not found in inventory!");
            return;
        }

        // Remove the item from the inventory
        m_Items.Remove(itemController.item);

        //Remove the item controller
        m_ItemControllers.Remove(itemController);

        //Callback when Item is removed from inventory
        itemController.OnRemovedFromInventory();
    }
}