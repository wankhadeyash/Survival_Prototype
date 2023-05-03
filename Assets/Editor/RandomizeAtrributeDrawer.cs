using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(RandomizeAttribute))]
public class RandomizeAtrributeDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 32f;
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.Float)
        {
            Rect lablePos = new Rect(position.x, position.y, position.width, 16);
            Rect buttonPos = new Rect(position.x, position.y + lablePos.height + 10, position.width, 16);
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.LabelField(position, label, new GUIContent(property.floatValue.ToString()));
            EditorGUI.EndProperty();
            if (GUI.Button(buttonPos, "Randomize"))
            {
                RandomizeAttribute randomizeAttribute = (RandomizeAttribute)attribute;
                property.floatValue = Random.Range(randomizeAttribute.minValue, randomizeAttribute.maxValue);
            }
        }
    }
}
