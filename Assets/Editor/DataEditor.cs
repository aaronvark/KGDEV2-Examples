using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Data))]
public class DataEditor : PropertyDrawer {
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		SerializedProperty name = property.FindPropertyRelative("name");
		SerializedProperty value = property.FindPropertyRelative("value");
		SerializedProperty myObject = property.FindPropertyRelative("myObject");
		SerializedProperty open = property.FindPropertyRelative("open");

		position.height = EditorGUIUtility.singleLineHeight;

		open.boolValue = EditorGUI.Foldout(position, open.boolValue, "Data");

		if (open.boolValue) {
			position.y += EditorGUIUtility.singleLineHeight;

			EditorGUI.indentLevel++;

			EditorGUI.PropertyField(position, name);
			position.y += EditorGUIUtility.singleLineHeight;
			EditorGUI.PropertyField(position, value);
			EditorGUI.indentLevel++;

			position.y += EditorGUIUtility.singleLineHeight;
			EditorGUI.PropertyField(position, myObject);

			EditorGUI.indentLevel--;
			EditorGUI.indentLevel--;
		}
	}

	public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
	{
		return EditorGUIUtility.singleLineHeight * 5;
	}
}

/*
 * 
[CustomPropertyDrawer(typeof(Data))]
public class DataEditor : PropertyDrawer {
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		
		SerializedProperty name = property.FindPropertyRelative("name");
		SerializedProperty value = property.FindPropertyRelative("value");
		SerializedProperty myObject = property.FindPropertyRelative("myObject");
		SerializedProperty open = property.FindPropertyRelative("open");

		position.height = EditorGUIUtility.singleLineHeight;

		open.boolValue = EditorGUI.Foldout(position, open.boolValue, "Data");

		if (open.boolValue) {
			position.y += EditorGUIUtility.singleLineHeight;

			EditorGUI.indentLevel++;

			EditorGUI.PropertyField(position, name);
			position.y += EditorGUIUtility.singleLineHeight;
			EditorGUI.PropertyField(position, value);
			position.y += EditorGUIUtility.singleLineHeight;
			EditorGUI.PropertyField(position, myObject);

			EditorGUI.indentLevel--;
		}
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		SerializedProperty open = property.FindPropertyRelative("open");
		if ( open.boolValue ) {
			return EditorGUIUtility.singleLineHeight * 4;
		}
		return EditorGUIUtility.singleLineHeight;
	}
}
*/