using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlankBrains.Inventory
{
    public class ItemPickUp : MonoBehaviour
    {
        [SerializeField] protected InventoryItemData m_Item; // Serialized field to hold a reference to the ScriptableObject item
        public InventoryItemData item => m_Item;

        public int m_StackSize = 1;

        public float m_PlayerDetectionRadius;
        public LayerMask m_PlayerMask;

        private void Update()
        {
            DetectPlayer();
        }

        //Using this way to detect because we are using character controller on our player and colliding with character controller doesn't
        //call OnCollisionEnter. Can't use Ontrigger coz want to detect collision with ground
        void DetectPlayer() 
        {
            Collider[] collider = Physics.OverlapSphere(transform.position, m_PlayerDetectionRadius, m_PlayerMask);
            foreach (Collider c in collider)
            {
                if (c.TryGetComponent<InventoryHolder>(out InventoryHolder inventoryHolder))
                {
                    if (inventoryHolder.InventoryManager.AddItem(m_Item, m_StackSize))
                        Destroy(gameObject);
                }
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, m_PlayerDetectionRadius);
        }
    }
}
