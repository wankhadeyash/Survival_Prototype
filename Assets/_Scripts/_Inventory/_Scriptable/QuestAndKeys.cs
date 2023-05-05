using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Quest items may have specific requirements to obtain or use them, such as completing a certain task or talking to a specific NPC.
[System.Flags]
public enum RequiredTasks 
{
    None
}
[CreateAssetMenu(fileName = "QuestAndKeys Item", menuName = "ScriptableObject/Inventory/QuestAndKeys")]
public class QuestAndKeys : Item
{
    public bool isUnique; // Only one present in game 
    public RequiredTasks requiredTasks;
}
