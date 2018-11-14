using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDataUser : MonoBehaviour {
	public MyCustomData data;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmos() {
		Gizmos.DrawIcon( transform.position, "ninja", true );
	}
}
