using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Inventory
{
    public static List<IInventoryObserver> m_Observers = new List<IInventoryObserver>();

    public static int m_MaxSlots;

    public static List<Item> m_Items = new List<Item>();

    public static void RegisterAsObserver(IInventoryObserver observer)
    {
        m_Observers.Add(observer);
    }

    public static void OnInventoryUpdated()
    {
        for (int i = 0; i < m_Observers.Count; i++)
            m_Observers[i].OnInventoryUpdated();
    }
}
