using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class does nothing, but serves as an example of how any script in your game
//might contain a public reference of your own Data types
public class GameScript : MonoBehaviour {
	public Data myData;

	private void MyVeryPrivateFunction() {
		Debug.Log ("Our intelligence has been stolen!");
	}
}
