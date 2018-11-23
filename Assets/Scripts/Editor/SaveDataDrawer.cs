using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SaveData))]
public class SaveDataDrawer : PropertyDrawer {
	bool foldout = false;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		foldout = EditorGUI.Foldout(position, foldout, "Save Data", true );
		if ( foldout ) {
			SerializedProperty keys = property.FindPropertyRelative("keys");
			SerializedProperty values = property.FindPropertyRelative("values");

			position.height = EditorGUIUtility.singleLineHeight;
			EditorGUI.indentLevel++;
			for( int i = 0; i < keys.arraySize; ++i ) {
				string key = keys.GetArrayElementAtIndex(i).stringValue;
				float value = values.GetArrayElementAtIndex(i).floatValue;

				position.y += EditorGUIUtility.singleLineHeight;

				EditorGUI.LabelField( position, key + " - " + value.ToString() );
			}
			EditorGUI.indentLevel--;
		}
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		if ( !foldout ) {
			return EditorGUIUtility.singleLineHeight;
		}
		
		//We can't access a dictionary from this point, because Unity can't serialize it (also because it's generic)
		//SerializedProperty dict = property.FindPropertyRelative("bunchOfData");

		//but we can access the Lists we added!
		SerializedProperty keys = property.FindPropertyRelative("keys");
		//SerializedProperty values = property.FindPropertyRelative("values");	//only need the one

		//+1 to include the foldout
		return ( keys.arraySize + 1 ) * EditorGUIUtility.singleLineHeight;
	}
}
