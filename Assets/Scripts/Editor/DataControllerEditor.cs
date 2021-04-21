using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DataController))]
public class DataControllerEditor : Editor
{
    public override void OnInspectorGUI() {
        DataController myTarget = (DataController)target;
        DrawDefaultInspector();
        //myTarget.experience = EditorGUILayout.IntField("Experience", myTarget.experience);
        //EditorGUILayout.LabelField("Level", myTarget.Level.ToString());

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        myTarget.levelName = EditorGUILayout.TextField("Level Name", myTarget.levelName);
        if (GUILayout.Button("Save"))
        {
            myTarget.SaveData();
        }
        if (GUILayout.Button("Load"))
        {
            myTarget.LoadData();
        }
    }
}