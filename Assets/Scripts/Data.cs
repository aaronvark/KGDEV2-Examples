using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This needs to be added to your own classes / structs if you want to be able to see them as
//a public variable from the editor
//NOTE: This only works if everything inside of the class ALSO has this (like Object here), or if they are atomic types: string, int, bool, float, etc.
[System.Serializable]
public class Data {
	public bool open;
	public string name;
	[Range(0,100)]
	public float value;
	public Object myObject;
}
