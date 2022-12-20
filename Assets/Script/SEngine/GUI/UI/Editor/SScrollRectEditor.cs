using SEngine.UI;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CanEditMultipleObjects, CustomEditor(typeof(SScrollRect), true)]
public class SScrollRectEditor :ScrollRectEditor
{
    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical("box");
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ParentScrollRect"));
        serializedObject.ApplyModifiedProperties();
        GUILayout.EndVertical();

        GUILayout.Space(10);

        base.OnInspectorGUI();
    }
}