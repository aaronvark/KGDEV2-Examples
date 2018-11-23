using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMyDataUser {
	void Start( );
	void Update( );
	UnityEngine.Vector3 DoSomething( System.Int32 x, System.Single y, System.Type whatever);
}
