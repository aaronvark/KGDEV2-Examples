using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//don't forget this, but make sure this script is in a folder named Editor
using UnityEditor;

//Links the GameScript class to this editor (GameScriptInspector)
[CustomEditor(typeof(GameScript))]
public class GameScriptInspector : Editor {
	//Controls the size of a custom window in the SceneView
	static Rect windowRect = new Rect (20, 20, 100, 100);
	GameScript targetObject;

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

	//This function gets called when the SceneView is rendered
	void OnSceneGUI() {
		//convert our target to a GameScript reference (this works because we are a CustomEditor(typeof(GameScript)))
		GameScript gameScriptTarget = target as GameScript;

		//Normal GUI doesn't work in the Scene View
		GUILayout.Label ("Dit werkt volgens mij niet.");

		//We can make a new window inside of it, and there normal GUI does work
		windowRect = GUI.Window (0, windowRect, TheWindowFunction, "Bovenaan");

		//We can ask which Tool the user has selected, and perform actions based on this
		switch (Tools.current) {
		case Tool.Scale:
			//The Handles class can create many UI-like interaction in 3D space for the Scene View
			//like this button, but also Move or Rotation handles

			//if (Handles.Button (Vector3.zero, Quaternion.identity, HandleUtility.GetHandleSize (Vector3.zero), 1f, Handles.CircleHandleCap)) {
			if (Handles.Button (gameScriptTarget.transform.position, Quaternion.identity, 1f, 1f, Handles.CircleHandleCap)) {
				Debug.Log ("Ingedrukt");
			}
			break;
		}

		//This is a hack to get rid of all the default tool controls
		//when you want to do something else completely. You could also bind this
		//to a specific button, like "press ` to go to our custom tool"
		//Here we wait for somebody to press the Rect mode when this object is selected,
		//then switch to the None tool
		if (Tools.current == Tool.Rect) {
			Tools.current = Tool.None;
		}
		//Once on the None tool, our custom tool code can take over
		else if (Tools.current == Tool.None) {
			//doe mijn eigen ding!
			//This makes sure we can't deselect the object with mouse clicks or keyboard events
			//to give us full control of what happens
			HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

			//An example of a Cube movehandle that updates the target object's position
			gameScriptTarget.transform.position = Handles.FreeMoveHandle (gameScriptTarget.transform.position, gameScriptTarget.transform.rotation, HandleUtility.GetHandleSize (gameScriptTarget.transform.position), Vector3.one, Handles.CubeHandleCap);
		}
	}

	//The custom window function. It receives an id
	//if you have more than one, you give each its own number, starting at 0
	void TheWindowFunction( int id ) {
		//Makes it so you can drag the window around
		GUI.DragWindow ();

		//Normal GUI works inside of this function
		if (GUILayout.Button ("Knop")) {
			//
		}

		//This makes sure that if the mouse is inside this box, we cannot click "through" it
		//by using the current event. If we don't, the Scene View background will catch the mouse-click
		//and deselect the object.
		if (Event.current.isMouse) {
			Event.current.Use ();
		}
	}

	//This function gets called when the object is deselected
	void OnDisable() {
		//Debug.Log ("Editor disabled!");
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