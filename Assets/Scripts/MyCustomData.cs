using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum CharacterPart {
	HEAD,
	TORSO,
	ARM_LEFT,
	ARM_RIGHT,
	LEG_LEFT,
	LEG_RIGHT
}

//Note that if you omit this, Unity (assumes it) cannot display or serialize this data
// - This doesn't mean it CAN be serialized (just that it should be)
[System.Serializable]
public struct MyCustomData {
	public CharacterPart part;
	public Texture2D textureAsset;
	[HideInInspector]
	public Vector3 position;
}
