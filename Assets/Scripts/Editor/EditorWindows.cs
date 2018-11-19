using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Rincewind : ScriptableWizard {

	[MenuItem("Category/Rincewind")]
	public static void Create() {
		//wizard open
		ScriptableWizard.DisplayWizard<Rincewind>("Rincewind");
	}

	void OnGUI() {
		if ( GUILayout.Button("Do it again") ) {
			Debug.Log("Nope 2");
		}
	}

	void OnWizardCreate() {
		Debug.Log("Nope");
	}
}

public class SomeEditor : EditorWindow {
	[MenuItem("Category/SomeWindow")]
	public static void Create() {
		EditorWindow.GetWindow<SomeEditor>();
	}
}
