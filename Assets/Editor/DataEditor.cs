using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//don't forget this, and make sure this script is in a folder called Editor
using UnityEditor;

//Links this PropertyDrawer (DataEditor) to the property it will draw (Data)
[CustomPropertyDrawer(typeof(Data))]
public class DataEditor : PropertyDrawer {

	//The main render function for this property
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		//retrieve the variables of the property we are drawing (an instance of Data)
		//see Data to confirm these names match the variables!
		SerializedProperty name = property.FindPropertyRelative("name");
		SerializedProperty value = property.FindPropertyRelative("value");
		SerializedProperty myObject = property.FindPropertyRelative("myObject");
		SerializedProperty open = property.FindPropertyRelative("open");

		//the Rect position is the "full size" of the draw area we are given to work with
		//but we want to render one line at a time, so we change the height
		position.height = EditorGUIUtility.singleLineHeight;

		//This managed and updates the foldout button, and it stores it in the Data object
		//so it will remember if it was open/closed when we view it again later
		open.boolValue = EditorGUI.Foldout(position, open.boolValue, "Data");

		//if it is open, render the other properties as well
		if (open.boolValue) {
			//move one line down (the foldout used a line), we'll do this after every property we draw
			position.y += EditorGUIUtility.singleLineHeight;

			//moves GUI elements one tab to the right
			EditorGUI.indentLevel++;

			EditorGUI.PropertyField(position, name);
			position.y += EditorGUIUtility.singleLineHeight;
			EditorGUI.PropertyField(position, value);

			position.y += EditorGUIUtility.singleLineHeight;
			EditorGUI.PropertyField(position, myObject);

			EditorGUI.indentLevel--;
		}
	}

	//This functions gets called to let you define how much space you need (in height) to render your GUI
	//Here we check if the foldout is open, if it is we need more space (4 lines), instead of just 1
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		SerializedProperty open = property.FindPropertyRelative("open");
		if ( open.boolValue ) {
			return EditorGUIUtility.singleLineHeight * 4;
		}
		return EditorGUIUtility.singleLineHeight;
	}
}