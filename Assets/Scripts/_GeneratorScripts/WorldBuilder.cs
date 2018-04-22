using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(ProcGenWorld))]
[CanEditMultipleObjects]
public class WorldBuilder : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ProcGenWorld myWorld = (ProcGenWorld)target;
        if (GUILayout.Button("Begin"))
            myWorld.CreateStart();
        if (GUILayout.Button("Generate"))
            myWorld.BuildWorld();
        if (GUILayout.Button("Clear Exits"))
            myWorld.ClearExits();
        if (GUILayout.Button("Destroy"))
            myWorld.DestroyWorld();
        if (GUILayout.Button("Create Player"))
            myWorld.CreatePlayer();
    }
}
#endif