using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemController : MonoBehaviour
{
    [Tooltip("Assign scriptable item object")]
    [SerializeField] protected Item m_Item; // Serialized field to hold a reference to the ScriptableObject item
    public Item item => m_Item;

    protected bool m_IsEquipped;
    protected KeyCode m_UseButton; // Protected variable to hold the key code for using the item

    private void Awake()
    {
        m_UseButton = m_Item.useButton; // Assign the use button from the ScriptableObject item on Awake
    }

    protected virtual void OnOnStartBefore() { } // Virtual method that gets called before Start()

    // Start is called before the first frame update
    void Start()
    {
        OnOnStartBefore(); // Call OnOnStartBefore() before Start()

        OnOnStartAfter(); // Call OnOnStartAfter() after Start()
    }

    protected virtual void OnOnStartAfter() { } // Virtual method that gets called after Start()

    protected virtual void OnOnUpdateBefore() { } // Virtual method that gets called before Update()

    // Update is called once per frame
    void Update()
    {
        OnOnUpdateBefore(); // Call OnOnUpdateBefore() before Update()

        GetInput(); // Call the GetInput() method to check for input

        OnOnUpdateAfter(); // Call OnOnUpdateAfter() after Update()
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }

    protected virtual void OnOnUpdateAfter() { } // Virtual method that gets called after Update()

    protected virtual void GetInput()
    {
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
    protected virtual void OnEquipped() 
    {
        m_IsEquipped = true;
    }

    //When item is unequipped
    protected virtual void OnUnequipped() 
    {
        m_IsEquipped = false;
    }

    //Callback when Item is added to inventory
    public virtual void OnAddedToInventory() { }

    //Callback when Item is removed from inventory
    public virtual void OnRemovedFromInventory() { }

}
