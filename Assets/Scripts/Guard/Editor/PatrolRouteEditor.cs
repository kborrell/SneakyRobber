using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PatrolRoute))]
public class PatrolRouteEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PatrolRoute patrol = (PatrolRoute)target;
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("+"))
        {
            patrol.AddPoint();
        }
        if (GUILayout.Button("-"))
        {
            patrol.RemovePoint();
        }
        EditorGUILayout.EndHorizontal();
    }
}
