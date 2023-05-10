using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlankBrains.Inventory;
using UnityEngine.UI;
public class BackpackInventoryDisplay : InventoryDisplay
{
    [SerializeField] GameObject m_BackpackUI;
    private bool m_IsInventoryOpen;

    protected override void Start()
    {
        base.Start();
        m_BackpackUI.SetActive(false);
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Tab))
            ToggleInventoryDisplay();
    }

    void ToggleInventoryDisplay()
    {
        m_IsInventoryOpen = m_IsInventoryOpen == false ? true : false;
        m_BackpackUI.SetActive(m_IsInventoryOpen);
    }
    public override void OnSlotButtonClicked(InventorySlot_UI slot)
    {
        //Check if inventory is opened
        if (!m_IsInventoryOpen)
            return;
        // Check if the UISlot has an item in it
        if (slot.AssignedInventorySlot.ItemData == null)
        {
            // Log a message if there is no item in the UISlot and return from the function
            Debug.Log($"No Item is slot");
            return;
        }
        // Update the MouseSlot to the clicked UISlot
        m_MouseSlot.UpdateUISlot(slot);
    }
}
