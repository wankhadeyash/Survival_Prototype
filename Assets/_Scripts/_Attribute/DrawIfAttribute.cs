using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawIfAttribute : PropertyAttribute
{
    public string comparedPropertyName;
    public object comparedValue;

    public DrawIfAttribute(string comparedValueName, object comparedValue) 
    {
        this.comparedPropertyName = comparedValueName;
        this.comparedValue = comparedValue;
    }
}
