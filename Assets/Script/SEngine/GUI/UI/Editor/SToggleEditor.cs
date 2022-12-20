using SEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CanEditMultipleObjects, CustomEditor(typeof(SToggle), true)]
public class SToggleEditor : ToggleEditor
{
    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical("box");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Text"));
        serializedObject.ApplyModifiedProperties();
        GUILayout.EndVertical();

        GUILayout.Space(10);

        base.OnInspectorGUI();
    }
}
