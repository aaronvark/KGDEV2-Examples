using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDataUser : MonoBehaviour {
	public MyCustomData data;
	
	// Use this for initialization
	public void Start () {
		
	}
	
	// Update is called once per frame
	public void Update () {
		
	}

	public Vector3 DoSomething( int x, float y, System.Type whatever ) {
		return Vector3.zero;
	}

	void OnDrawGizmos() {
		Gizmos.DrawIcon( transform.position, "ninja", true );
	}
}
