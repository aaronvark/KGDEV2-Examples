using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(GameScript))]
public class GameScriptInspector : Editor {
	public override void OnInspectorGUI ()
	{
		serializedObject.Update ();

		if (GUILayout.Button ("Rincewind", EditorStyles.miniButton, GUILayout.Width(150))) {
			//doe iets met dit ding!
			Debug.Log("Humans are faster than horses over short distances");
			//ScriptableWizard.GetWindow<Rincewind> ();
		}
		DrawPropertiesExcluding (serializedObject, "m_Script");

		serializedObject.ApplyModifiedProperties ();
	}
}


public class Rincewind : ScriptableWizard {
	[MenuItem("Some Menu/Rincewind")]
	public static void Open() {
		ScriptableWizard.GetWindow<Rincewind> ();
	}

	protected override bool DrawWizardGUI ()
	{
		return base.DrawWizardGUI ();
	}

	void OnWizardCreate() {
		Debug.Log ("BOOM");
	}
}

public class SomeEditor : EditorWindow {
	[MenuItem("Some Menu/Editor Window")]
	public static void Open() {
		EditorWindow.GetWindow<SomeEditor> ();
	}

	void OnGUI() {
		GUILayout.Label ("Hoi");
	}
}