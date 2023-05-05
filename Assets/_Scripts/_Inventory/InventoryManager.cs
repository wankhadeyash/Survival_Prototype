using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public interface IInventoryObserver 
{
    void OnInventoryUpdated();
}


public class InventoryManager : MonoBehaviour,IInventoryObserver
{
    // The maximum number of slots available in the inventory
    public int m_MaxSlots = 15;

    List<ItemController> m_ItemControllers = new List<ItemController>();


    private void Awake()
    {
        Inventory.m_MaxSlots = m_MaxSlots;
        Inventory.RegisterAsObserver(this);
    }

    // Add an item to the inventory
    public void AddItem(ItemController itemController)
    {
        // Check if there is space in the inventory
        if (Inventory.m_Items.Count >= m_MaxSlots)
        {
            Debug.Log("Inventory is full!");
            return;
        }
        // Add the item to the inventory
        Inventory.m_Items.Add(itemController.item);

        //Add th itemController
        m_ItemControllers.Add(itemController);

        //Callback when Item is added to inventory
        itemController.OnAddedToInventory();

        //Callback to inventory which in turns calls all the resgistred interfaces
        Inventory.OnInventoryUpdated();

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
        Inventory.m_Items.Remove(itemController.item);

        //Remove the item controller
        m_ItemControllers.Remove(itemController);

        //Callback when Item is removed from inventory
        itemController.OnRemovedFromInventory();

        //Callback to inventory which in turns calls all the resgistred interfaces
        Inventory.OnInventoryUpdated();
    }

    public void OnInventoryUpdated()
    {
        
    }
}