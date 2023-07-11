using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace BlankBrains.Inventory
{
    public class InventorySlot_UI : MonoBehaviour, IPointerDownHandler
    {
        // Reference to the image component used to display the item icon.
        public Image m_Image;

        public TextMeshProUGUI m_ItemCountText;
        public TextMeshProUGUI m_ItemDiscriptionText;

        // Reference to the InventorySlot this UI slot is assigned to.
        [SerializeField] InventorySlot m_AssignedInventorySlot;

        // Property to get the assigned InventorySlot.
        public InventorySlot AssignedInventorySlot => m_AssignedInventorySlot;

        // Reference to the parent InventoryDisplay component.
        InventoryDisplay m_InventoryDisplay;
        public InventoryDisplay InventoryDisplay => m_InventoryDisplay;


        private void Awake()
        {
            // Initialize the slot image and button components.
            ClearSlot();

            // Get a reference to the parent InventoryDisplay component.
            m_InventoryDisplay = GetComponentInParent<InventoryDisplay>();
        }

        // Handle pointer down events for this UI slot.
        public void OnPointerDown(PointerEventData eventData)
        {
            // Notify the parent InventoryDisplay component that this UI slot button was clicked.
            m_InventoryDisplay.OnSlotButtonClicked(this);
        }

        // Initialize this UI slot with an assigned InventorySlot.
        public void Init(InventorySlot slot)
        {
            m_AssignedInventorySlot = slot;
        }

        // Update this UI slot with the assigned InventorySlot's data.
        public void UpdateUISlot(InventorySlot slot)
        {
            m_AssignedInventorySlot = slot;
            // Update the parent InventoryDisplay's SlotDictionary with this UI slot and the assigned InventorySlot.
            m_InventoryDisplay.SlotDictionary[this] = slot;
            // Update the UI slot image with the assigned InventorySlot's icon sprite.
            m_Image.sprite = slot.ItemData.data.icon; ;
            //Update item count with assigned InventorySlot's 
            m_ItemCountText.text = slot.StackSize.ToString();
            //Update item discription 
            m_ItemDiscriptionText.text = slot.ItemData.data.discription;

        }

        //Called when avatar selection
        public void UpdateUISlot(ItemData itemData) 
        {
            m_Image.sprite = itemData.icon;

            m_ItemCountText.text = "";
            m_ItemDiscriptionText.text = itemData.discription;


        }

        // Update this UI slot with its assigned InventorySlot's data.
        public void UpdateUISlot()
        {
            // If this UI slot has an assigned InventorySlot, update its data.
            if (m_AssignedInventorySlot != null)
            {
                UpdateUISlot(m_AssignedInventorySlot);
            }
        }

        // Clear this UI slot's assigned InventorySlot and image.
        public void ClearSlot()
        {
            m_AssignedInventorySlot?.ClearSlot();
            m_Image.sprite = null;
            m_ItemCountText.text = "0";
          //  m_ItemDiscriptionText.text = "";
        }
    }
}
