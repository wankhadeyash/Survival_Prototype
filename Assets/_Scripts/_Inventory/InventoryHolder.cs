using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Each entity in game should attach this invetory holder
//For e.g. players backs will have one and player HUD will have one
[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] protected int m_InventorySize;
    public int InventorySize => m_InventorySize;

    //Manager associated with this holder
    [SerializeField] protected InventoryManager m_InventoryManager;
    public InventoryManager InventoryManager => m_InventoryManager;

    private void Awake()
    {
        m_InventoryManager = new InventoryManager(m_InventorySize);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //    m_InventoryManager.SaveInventoryData();
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    m_InventoryManager.LoadInventoryData();   
        //}
    }

}
