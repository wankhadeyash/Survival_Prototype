using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class MouseSlot : MonoBehaviour
{
    public Image m_Image;
    [SerializeField] InventorySlot_UI m_AssignedInventoryUISlot;
    public InventorySlot_UI AssignedInventorySlot => m_AssignedInventoryUISlot;
    private bool m_FoundSlot;

    public void Awake()
    {
        m_Image.color = Color.clear;
    }

    private void Update()
    {
        if (m_AssignedInventoryUISlot != null && Input.GetMouseButton(0))
            transform.position = Input.mousePosition;
        if (m_AssignedInventoryUISlot != null && Input.GetMouseButtonUp(0))
        {
            if (IsPointerOverUIObject(out List<RaycastResult> results))
            {
                foreach (RaycastResult result in results) 
                {
                    //If dropped over slot
                    if (result.gameObject.TryGetComponent<InventorySlot_UI>(out InventorySlot_UI slotUI))
                    {
                        if (slotUI == null)
                            Debug.Log("Slout UI is null");
                        if(slotUI.AssignedInventorySlot == null)
                            Debug.Log("Slout UI Assigned is null");
                        if(slotUI.AssignedInventorySlot.ItemData == null)
                            Debug.Log("Slout UI Assigned Item Data is null");   
                        if(m_AssignedInventoryUISlot == null)
                            Debug.Log(" Assigned Item  is null");
                        //If slot is empty or dropping on same slot
                        if (slotUI.AssignedInventorySlot.ItemData == null && slotUI != m_AssignedInventoryUISlot)
                        {
                            Debug.Log("Assigning new slot");
                            m_FoundSlot = true;
                            //Set item data on new UI slot
                            Debug.Log(slotUI.name);
                            slotUI.AssignedInventorySlot.UpdateSlot(m_AssignedInventoryUISlot.AssignedInventorySlot.ItemData,
                                m_AssignedInventoryUISlot.AssignedInventorySlot.StackSize);
                            slotUI.UpdateUISlot();
                            //Clear selected UI slot
                            m_AssignedInventoryUISlot.ClearSlot();
                            ClearSlot();
                            break;
                        }
                        //If dropping on same slot do nothing just clear the mouse slot
                        else if(slotUI == m_AssignedInventoryUISlot) 
                        {
                            ClearSlot();

                        }

                        //If slot is not empty switch 
                        else
                        {
                            Debug.Log("Switching slot");
                            m_FoundSlot = true;
                            break;
                        }

                    }
                    else 
                    {
                        m_FoundSlot = false;
                    }
                }
            }
            else if(!m_FoundSlot)
            {
                Debug.Log("Dropping item");
                ClearSlot();
            }
        }
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
