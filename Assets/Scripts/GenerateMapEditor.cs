using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameMapController))]
public class GenerateMapEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        GameMapController mapController = (GameMapController)target;

        if (GUILayout.Button("generate")) {
            mapController.generateObjects();
        }

        if (GUILayout.Button("clear")) {
            mapController.clearGameObjectsInEditor();
        }

    }
	
}
