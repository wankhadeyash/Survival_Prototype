using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using System.Reflection;

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
        comparedField = property.serializedObject.FindProperty(drawIf.comparedPropertyName);
        if (comparedField == null)
        {
            Debug.LogError($"compared field variable name spelled wrong");
            return;
        }
        //comparedField = property.serializedObject.FindProperty(path);

        if (ShowMe(comparedField))
        {
            EditorGUI.PropertyField(position, property, label);
        }

    }

    bool ShowMe(SerializedProperty comparedField)
    {
        switch (comparedField.type)
        {
            case "bool":
                return this.comparedField.boolValue.Equals(drawIf.comparedValue);
            case "Enum":
                var drawIfVals = drawIf.comparedValue as object[];
                if (drawIfVals == null)
                {
                    int enumValue = comparedField.enumValueFlag;
                        return (enumValue & (int)drawIf.comparedValue) == (int)drawIf.comparedValue;
                }
                return false;
        }
        return false;
    }
}
