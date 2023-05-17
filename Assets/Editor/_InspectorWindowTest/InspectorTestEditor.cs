using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(InspectorTest))]
public class InspectorTestEditor : Editor
{
    SerializedObject so;
    SerializedProperty radius;


    
    //private void OnEnable()
    //{
    //    so = serializedObject;
    //    radius = so.FindProperty("m_Radius");
    //}


    
    //public override void OnInspectorGUI()
    //{
    //    so.Update();
    //    EditorGUILayout.PropertyField(radius);
    //    so.ApplyModifiedProperties();
    //}
}
