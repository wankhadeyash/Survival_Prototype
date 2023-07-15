using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace BlankBrains.Inventory
{
    public abstract class ItemController : NetworkBehaviour
    {
        [Tooltip("Assign scriptable item object")]
        [SerializeField] protected InventoryItemData m_Item; // Serialized field to hold a reference to the ScriptableObject item
        public InventoryItemData item => m_Item;

        protected bool m_IsEquipped;
        protected KeyCode m_UseButton; // Protected variable to hold the key code for using the item
        protected KeyCode m_PickUpButton; // If pickup type is manual


        //Components
        private ItemPickUp m_ItemPickUp;
        private Rigidbody m_Rb;
        private MeshRenderer m_MeshRenderer;
        private Collider[] m_Colliders;
        private NetworkObjectFollow m_NetworkObjectFollow;

        private void Awake()
        {
            m_UseButton = m_Item.useButton; // Assign the use button from the ScriptableObject item on Awake
            m_PickUpButton = m_Item.itemPickupType == ItemPickupType.Manual ? m_Item.pickUpButton : KeyCode.None;
        }

        protected virtual void OnOnStartBefore() { } // Virtual method that gets called before Start()

        // Start is called before the first frame update
        void Start()
        {

            OnOnStartBefore(); // Call OnOnStartBefore() before Start()

            OnOnStartAfter(); // Call OnOnStartAfter() after Start()
        }

        public override void OnNetworkSpawn()
        {

            //Get required components
            m_ItemPickUp = GetComponent<ItemPickUp>();
            m_Rb = GetComponent<Rigidbody>();
            m_MeshRenderer = GetComponent<MeshRenderer>();
            m_Colliders = GetComponents<Collider>();
            m_NetworkObjectFollow = GetComponent<NetworkObjectFollow>();

            EnablePickUpComponents();
        }

        protected virtual void OnOnStartAfter() { } // Virtual method that gets called after Start()

        protected virtual void OnOnUpdateBefore() { } // Virtual method that gets called before Update()

        // Update is called once per frame
        void Update()
        {
            if (!IsOwner)
                return;
            OnOnUpdateBefore(); // Call OnOnUpdateBefore() before Update()

            UseItemInput(); // Call the GetInput() method to check for Use item input

            PickUpItemInput(); // Call the GetInput() method to check for pickup item input

            OnOnUpdateAfter(); // Call OnOnUpdateAfter() after Update()


        }

        protected virtual void OnOnUpdateAfter() { } // Virtual method that gets called after Update()

        protected virtual void PickUpItemInput()
        {

        }

        protected virtual void UseItemInput()
        {
            if (!m_IsEquipped)
                return;
            switch (m_Item.useButtonType) // Check the use button type from the ScriptableObject item
            {
                case ItemUseButtonType.ButtonPress: // If use button type is button press
                    if (Input.GetKeyDown(m_UseButton)) // Check if use button is pressed down
                        UseItem(); // Call the UseItem() method to use the item
                    break;
                case ItemUseButtonType.ButtonHold: // If use button type is button hold
                    if (Input.GetKey(m_UseButton)) // Check if use button is held down
                        UseItem(); // Call the UseItem() method to use the item
                    break;
                case ItemUseButtonType.ButtonRelease: // If use button type is button release
                    if (Input.GetKeyUp(m_UseButton)) // Check if use button is released
                        UseItem(); // Call the UseItem() method to use the item
                    break;
            }
        }

        // An abstract method to perform the item's primary task.
        protected abstract void UseItem(); // Abstract method that gets implemented by child classes to use the item

        // An abstract method to perform any secondary tasks associated with the item.
        protected abstract void DoSecondaryTask(); // Abstract method that gets implemented by child classes to perform secondary tasks associated with the item

        //When item is equipped
        public virtual void OnEquipped(Transform equipPosition)
        {
            m_NetworkObjectFollow.followObject = equipPosition;

            OnEquippedServerRPC();

        }

        [ServerRpc(RequireOwnership = false)]
        void OnEquippedServerRPC()
        {
            OnEquippedClientRPC();
        }

        [ClientRpc]
        void OnEquippedClientRPC()
        {
            m_MeshRenderer.enabled = true;


            m_IsEquipped = true;
        }
        //When item is unequipped
        public virtual void OnUnequipped()
        {
            m_NetworkObjectFollow.followObject = null;
            OnUnequippedServerRPC();


        }

        [ServerRpc(RequireOwnership = false)]
        void OnUnequippedServerRPC() 
        {
            OnUnequippedClientRPC();
        }

        [ClientRpc]
        void OnUnequippedClientRPC() 
        {
            m_IsEquipped = false;

            m_MeshRenderer.enabled = false;

        }

        //Callback when item is added in inventory
        public virtual void OnItemAddedToInventory(Transform playerNetworkObject) 
        {
            OnItemAddedToInventoryServerRPC(playerNetworkObject.GetComponent<NetworkObject>());
        }

        [ServerRpc(RequireOwnership = false)]
        void OnItemAddedToInventoryServerRPC(NetworkObjectReference playerNetworkObjectReference) 
        {
            OnItemAddedToInventoryClientRPC(playerNetworkObjectReference);
        }

        [ClientRpc]
        void OnItemAddedToInventoryClientRPC(NetworkObjectReference playerNetworkObjectReference) 
        {
            MultiplayerSpawnManager.Instance.SetObjectsParentServerRPC(gameObject.GetComponent<NetworkObject>(), playerNetworkObjectReference);
            playerNetworkObjectReference.TryGet(out NetworkObject player);
            gameObject.transform.position = player.transform.position;
            gameObject.transform.rotation = player.transform.rotation;

            DisablePickUpComponents();
        }
        //Callback when item is removed from inventory
        public virtual void OnItemRemovedFromInventory() 
        {
            //  m_IsEquipped = false;
            OnItemRemovedFromInventoryServerRPC();
        }

        [ServerRpc(RequireOwnership = false)]
        void OnItemRemovedFromInventoryServerRPC() 
        {
            OnItemRemovedFromInventoryClientRPC();
        }

        [ClientRpc]
        void OnItemRemovedFromInventoryClientRPC() { }


        public virtual void OnItemDroppedFromInventory(Transform dropPosition) 
        {

            gameObject.GetComponent<NetworkObject>().TryRemoveParent();
            gameObject.transform.position = dropPosition.position;
            gameObject.transform.rotation = dropPosition.rotation;

            OnItemDroppedFromInventoryServerRPC();
        }

        [ServerRpc(RequireOwnership = false)]
        void OnItemDroppedFromInventoryServerRPC() 
        {
            OnItemDroppedFromInventoryClientRPC();
        }

        [ClientRpc]
        void OnItemDroppedFromInventoryClientRPC() 
        {
            EnablePickUpComponents();
            m_IsEquipped = false;

        }

        private void EnablePickUpComponents() 
        {
            m_MeshRenderer.enabled = true;
            m_ItemPickUp.enabled = true;
            m_Rb.isKinematic = false;

            foreach (Collider c in m_Colliders) 
            {
                c.enabled = true;
            }
            m_IsEquipped = false;
        }

        private void DisablePickUpComponents()
        {
            m_MeshRenderer.enabled = false;
            m_ItemPickUp.enabled = false;
            m_Rb.isKinematic = true;

            foreach (Collider c in m_Colliders)
            {
                c.enabled = false;
            }
        }
    }
}
