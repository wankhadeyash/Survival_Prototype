using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(DrawIfAttribute))]
public class DrawIfAttributeDrawer : PropertyDrawer
{
    // Reference to the attribute on the property.
    DrawIfAttribute drawIf;

    // Field that is being compared.
    SerializedProperty comparedField;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        drawIf = attribute as DrawIfAttribute;
        string path = System.IO.Path.ChangeExtension(property.propertyPath, drawIf.comparedPropertyName);
        comparedField = property.serializedObject.FindProperty(drawIf.comparedPropertyName);
        //comparedField = property.serializedObject.FindProperty(path);

        if (ShowMe(comparedField)) 
        {
            EditorGUI.PropertyField(position, property, label);
        }
        
    }

    bool ShowMe(SerializedProperty comapredField) 
    {
        switch (comapredField.type) 
        {
            case "bool":
                return comparedField.boolValue.Equals(drawIf.comparedValue);
            case "Enum":
                try 
                {
                    var drawIfVals = drawIf.comparedValue as object[];
                    if (drawIfVals == null)
                        return comapredField.intValue.Equals((int)drawIf.comparedValue);
                    if (drawIfVals.Length == 0)
                        return true;
                    foreach (object val in drawIfVals) 
                    {
                        if (comapredField.intValue.Equals((int)val))
                            return true;
                    }
                    return true;
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e.GetType() + ": trying to cast drawIf value to enum array when no enum array was passed");
                    return true;
                }
        }
        return false;
    }
}
