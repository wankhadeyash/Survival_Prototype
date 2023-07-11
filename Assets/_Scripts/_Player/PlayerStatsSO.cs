using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[CreateAssetMenu(menuName = "ScriptableObject/Player/Stats")]

public class PlayerStatsSO : ScriptableObject
{
    public Stat startingHealth;
    public Stat maxHealth;

    public Stat walkSpeed;
    public Stat runSpeed;
    public Stat speedChangeRate;

    public Stat jumpHeight;

    public Stat stamina;
    public Stat armor;

}

