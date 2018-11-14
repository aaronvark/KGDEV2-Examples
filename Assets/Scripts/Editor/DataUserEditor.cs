using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MyDataUser))]
public class DataUserEditor : Editor {
	private Rect windowPosition = new Rect(10, 20, 100, 100);
	private MyDataUser myTarget;

	void OnEnable() {
		myTarget = target as MyDataUser;
	}

	public override void OnInspectorGUI() {
		//DrawDefaultInspector();
		//handy when you want a clean UI, by omitting certain standard items
		DrawPropertiesExcluding( serializedObject, "m_Script");	//this is the script selection box

		//need this if we use the excluded properties
		serializedObject.ApplyModifiedProperties();
	}

	public void OnSceneGUI() {
		//make your own custom window at a certain position, a GUI function, and a title
		windowPosition = GUI.Window(0, windowPosition, MyWindow, "Some Window");
	
		//Bit of a hack
		//If we switch to scale tool, and set tool to none
		//This allows us to completely change the behaviour when in scale mode
		if (Tools.current == Tool.Scale ) {
			Tools.current = Tool.None;
		}

		//Then, if we see "Tool.None", we can do our custom stuff
		//To make it less hacky, you could store in which mode you were when you entered None,
		// - and use a custom function for this
		//scales handle with position relative to scene camera
		if ( Tools.current == Tool.None ) {
			float handleSize = HandleUtility.GetHandleSize(myTarget.data.position) * .1f;
			//draw a custom handle type
			myTarget.data.position = Handles.FreeMoveHandle(myTarget.data.position, Quaternion.identity, handleSize, Vector3.zero, Handles.CircleHandleCap );

			//prevent deselection, very useful for in-scene painting tools
			HandleUtility.AddDefaultControl(0);
		}
	}

	//custom GUI function, id allows you to manage which version of it you're drawing
	void MyWindow( int id ) {
		GUILayout.Label("BLA");
		if ( GUILayout.Button("Do it") ) {
			Debug.Log("Done did it");
		}

		//allows you to drag this window
		GUI.DragWindow();
	}
}
