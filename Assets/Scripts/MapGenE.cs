using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Map))]
public class MapGenE : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Map MyGen = (Map)target;
        if(GUILayout.Button("���� �����մϴ�."))
        {
            MyGen.BuilGenerator();
        }
    }
}
