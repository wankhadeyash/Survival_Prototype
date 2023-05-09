using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] protected InventoryItemData m_Item; // Serialized field to hold a reference to the ScriptableObject item
    public InventoryItemData item => m_Item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Entered player");
            if (other.TryGetComponent<InventoryHolder>(out InventoryHolder inventoryHolder))
            {
                if (inventoryHolder.InventoryManager.AddItem(m_Item))
                    Destroy(gameObject);
            }
        }
    }
}
