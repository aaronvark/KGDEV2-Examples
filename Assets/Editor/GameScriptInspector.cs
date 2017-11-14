using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//don't forget this, but make sure this script is in a folder named Editor
using UnityEditor;

//Links the GameScript class to this editor (GameScriptInspector)
[CustomEditor(typeof(GameScript))]
public class GameScriptInspector : Editor {
	public override void OnInspectorGUI ()
	{
		//get the latest state of the object we are an editor for (an instance of GameScript)
		serializedObject.Update ();

		//draw a button!
		if (GUILayout.Button ("Rincewind", EditorStyles.miniButton, GUILayout.Width(150))) {
			//doe iets met dit ding!
			Debug.Log("Humans are faster than horses over short distances");
			ScriptableWizard.GetWindow<Rincewind> ();
		}

		//Render the rest of the properties of the GameScript instance, but leave out the "m_Script" variable (because it's unnecessary)
		DrawPropertiesExcluding (serializedObject, "m_Script");

		//Apply whatever changed to the GameScript instance, (input fields edited, dragging things onto public properties, etc.)
		serializedObject.ApplyModifiedProperties ();
	}
}

//If you don't know Rincewind, go read some Terry Pratchett
public class Rincewind : ScriptableWizard {

	//Creates a menu item so we can easily reach this window
	//It's public static so we can also do this from code anywhere
	[MenuItem("Some Menu/Rincewind")]
	public static void Open() {
		ScriptableWizard.GetWindow<Rincewind> ();
	}

	//Write GUI code here to modify the wizard
	protected override bool DrawWizardGUI ()
	{
		return base.DrawWizardGUI ();
	}

	//This "magic" (Unity looks for it automatically) function gets called when you hit the default Create button
	void OnWizardCreate() {
		Debug.Log ("BOOM");
	}
}

//Similar to a wizard, but without the default create button
public class SomeEditor : EditorWindow {
	[MenuItem("Some Menu/Editor Window")]
	public static void Open() {
		EditorWindow.GetWindow<SomeEditor> ();
	}

	void OnGUI() {
		GUILayout.Label ("Hoi");
	}
}