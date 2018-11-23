using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FastMemorySave))]
public class FastMemorySaveEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		if ( GUILayout.Button("Load") ) { 
			FastMemorySave fms = target as FastMemorySave;
			fms.LoadFromFile();
			fms.saveData.OnAfterDeserialize();
			//serializedObject.ApplyModifiedProperties();
		}
	}
}
