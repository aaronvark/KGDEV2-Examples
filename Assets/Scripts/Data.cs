using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data {
	public bool open;
	public string name;
	[Range(0,100)]
	public float value;
	public Object myObject;
}
