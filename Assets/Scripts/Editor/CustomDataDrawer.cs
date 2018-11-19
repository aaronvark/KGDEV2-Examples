using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(MyCustomData))]
public class CustomDataDrawer : PropertyDrawer {
	static GUIContent emptyLabel = new GUIContent("");

	//does the actual drawing code
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		//Note you can use EditorStyles to do this!
		EditorGUI.LabelField(position, "Character Part", EditorStyles.boldLabel);

		//you could write a function like this to make it easier to work with the Rect position
		MoveDown( EditorGUIUtility.singleLineHeight, ref position );

		//Property is the "MyCustomData" object, inside of which we have a "name" and a "textureAsset"
		// - You can find nested values inside SerializedProperties with FindPropertyRelative
		SerializedProperty part = property.FindPropertyRelative("part");
		SerializedProperty textureAsset = property.FindPropertyRelative("textureAsset");

		EditorGUI.indentLevel++;

		//Then use a PropertyField to let Unity take care of the visual details
		//left half
		position.width *= .5f;
		position.width -= 5;
		EditorGUI.PropertyField( position, part, emptyLabel );

		//right half
		position.x += position.width + 10;
		EditorGUI.PropertyField( position, textureAsset, emptyLabel );

		EditorGUI.indentLevel--;
	}

	//should return how high the property will be (take into account foldouts!)
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		return EditorGUIUtility.singleLineHeight * 2;
	}

	void MoveDown( float h, ref Rect position ) {
		position.y += h;
		position.height -= h;
	}
}
