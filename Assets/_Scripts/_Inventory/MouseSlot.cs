using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace BlankBrains.Inventory
{
    public class MouseSlot : MonoBehaviour
    {
        public Image m_Image;
        [SerializeField] InventorySlot_UI m_AssignedInventoryUISlot;
        public InventorySlot_UI AssignedInventorySlot => m_AssignedInventoryUISlot;

        public void Awake()
        {
            m_Image.color = Color.clear;
        }

        private void Update()
        {
            if (m_AssignedInventoryUISlot != null && Input.GetMouseButton(0))
                transform.position = Input.mousePosition;
            if (m_AssignedInventoryUISlot != null && Input.GetMouseButtonUp(0))
                CheckForAssignNewSlot();
        }

        void CheckForAssignNewSlot()
        {
            if (IsPointerOverUIObject(out List<RaycastResult> results))
            {
                foreach (RaycastResult result in results)
                {
                    //If dropped over slot
                    if (result.gameObject.TryGetComponent<InventorySlot_UI>(out InventorySlot_UI newSlotUI))
                    {
                        //If slot is empty or dropping on same slot
                        if (newSlotUI.AssignedInventorySlot.ItemData == null && newSlotUI != m_AssignedInventoryUISlot)
                        {
                            Debug.Log("Assigning new slot");
                            //Set item data on new UI slot
                            Debug.Log(newSlotUI.name);
                            newSlotUI.AssignedInventorySlot.UpdateSlot(m_AssignedInventoryUISlot.AssignedInventorySlot.ItemData,
                                m_AssignedInventoryUISlot.AssignedInventorySlot.StackSize);
                            newSlotUI.UpdateUISlot();
                            //Clear selected UI slot
                            m_AssignedInventoryUISlot.ClearSlot();
                            //ClearSlot();
                            break;
                        }

                        //If slot is not empty switch and not dropping on same slot
                        else if (newSlotUI != m_AssignedInventoryUISlot)
                        {
                            Debug.Log("Switching slot");
                            SwitchSlots(m_AssignedInventoryUISlot, newSlotUI);
                            break;
                        }

                    }
                }
            }
            //Dropping item
            else
            {
                m_AssignedInventoryUISlot.InventoryDisplay.InventoryManager.DropItemFromSlot(m_AssignedInventoryUISlot.AssignedInventorySlot);
                m_AssignedInventoryUISlot.ClearSlot();
            }


            ClearSlot();
        }

        void SwitchSlots(InventorySlot_UI slot1, InventorySlot_UI slot2)
        {
            Debug.Log($"{slot1.name} {slot2.name}");

            InventoryItemData temItemData = slot1.AssignedInventorySlot.ItemData;
            int tempStackSize = slot1.AssignedInventorySlot.StackSize;

            //Switching slot1 to slot 2
            slot1.AssignedInventorySlot.UpdateSlot(slot2.AssignedInventorySlot.ItemData, slot2.AssignedInventorySlot.StackSize);
            slot1.UpdateUISlot();

            //Switching slo2 to slot1
            slot2.AssignedInventorySlot.UpdateSlot(temItemData, tempStackSize);
            slot2.UpdateUISlot();

        }

        public static bool IsPointerOverUIObject(out List<RaycastResult> results)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = Input.mousePosition;
            results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

        public void UpdateUISlot(InventorySlot_UI ui_Slot)
        {
            m_AssignedInventoryUISlot = ui_Slot;
            m_Image.color = Color.white;
            m_Image.sprite = ui_Slot.AssignedInventorySlot.ItemData.icon;
        }

        public void ClearSlot()
        {
            m_AssignedInventoryUISlot = null;
            m_Image.color = Color.clear;
            m_Image.sprite = null;
        }
    }
}