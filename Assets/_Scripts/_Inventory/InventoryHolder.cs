using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


namespace BlankBrains.Inventory
{
    //Each entity in game should attach this inventory holder
    //For e.g. players backs will have one and player HUD will have one
    [System.Serializable]
    public class InventoryHolder : NetworkBehaviour
    {
        [SerializeField] protected int m_InventorySize;
        public int InventorySize => m_InventorySize;

        [SerializeField] protected Transform m_EuipeItemPosition;
        public Transform EquipeItemPosition => m_EuipeItemPosition;

        [SerializeField] protected Transform m_DropItemPoisition;
        public Transform DropItemPosition => m_DropItemPoisition;

        [SerializeField] private GameObject m_PlayerNetworkObject;
        public GameObject PlayerNetworkObject => m_PlayerNetworkObject;

        //Manager associated with this holder
        [SerializeField] protected InventoryManager m_InventoryManager;
        public InventoryManager InventoryManager => m_InventoryManager;

        public override void OnNetworkSpawn()
        {
            m_InventoryManager = new InventoryManager(m_InventorySize,m_EuipeItemPosition,m_DropItemPoisition, m_PlayerNetworkObject);
            
        }
    }
}
