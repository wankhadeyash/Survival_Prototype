using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlankBrains.Inventory;
using UnityEngine.UI;
public class BackpackInventoryDisplay : InventoryDisplay
{
    [SerializeField] GameObject m_BackpackUI;
    private bool m_IsInventoryOpen;

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
        throw new System.NotImplementedException();
    }
}
