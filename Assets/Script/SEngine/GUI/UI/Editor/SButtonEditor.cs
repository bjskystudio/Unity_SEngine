using SEngine.UI;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CanEditMultipleObjects, CustomEditor(typeof(SButton), true)]
public class SButtonEditor : ButtonEditor
{
    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical("box");
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("IgnoreClickInterval"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("BtnText"));
        serializedObject.ApplyModifiedProperties();
        GUILayout.EndVertical();

        GUILayout.Space(10);

        base.OnInspectorGUI();
    }
}