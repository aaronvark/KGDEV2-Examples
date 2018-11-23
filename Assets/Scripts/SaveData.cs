using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

[System.Serializable]
public class SaveData : ISaveData, /*ISerializable,*/ ISerializationCallbackReceiver {
	//Dictionary usually can't be serialized... so we do this ourselves
	[System.NonSerialized]
	Dictionary<string,float> bunchOfData = new Dictionary<string,float>();

	[SerializeField]
	public List<string> keys = new List<string>();
	[SerializeField]
	public List<float> values = new List<float>();

	public void SetValue(string key, float value ) {
		if ( bunchOfData.ContainsKey( key )  ) {
			bunchOfData[key] = value;
		}
		else {
			bunchOfData.Add( key, value );
		}
	}

	//Unity-specific solution
	public void OnBeforeSerialize() {
		keys = new List<string>();
		values = new List<float>();

		foreach( KeyValuePair<string,float> pair in bunchOfData ) {
			keys.Add(pair.Key);
			values.Add(pair.Value);
		}
	}

	public void OnAfterDeserialize() {
		bunchOfData = new Dictionary<string,float>();

		if ( keys.Count != values.Count ) {
			Debug.LogError("Mismatched key/value pairs");
		}

		if ( keys.Count > 0 ) {
			for( int i = 0; i < keys.Count; ++i ) {
				bunchOfData.Add(keys[i], values[i]);
			}
		}
	}

	//This is how you could implement ISerializable yourself, but there is another way to do this in Unity
	#region ISerializable
	/*
	public SaveData( SerializationInfo info, StreamingContext context ) {
		//read the data we serialized exactly as we wrote it
		int count = (int)info.GetValue("count", typeof(int));

		bunchOfData = new Dictionary<string,float>();
		for( int i = 0; i < count; ++i ) {
			string key = (string)info.GetValue("key"+i, typeof(string));
			float value = (float)info.GetValue("value"+i, typeof(float));
			bunchOfData.Add(key, value);
		}
	}

	public void GetObjectData( SerializationInfo info, StreamingContext context ) {
		//important thing here is to do everything in the same order, with the same names
		int count = bunchOfData.Count;
		info.AddValue("count", count);

		//keep count to have unique names for each key & value
		int i = 0;
		foreach( KeyValuePair<string,float> pair in bunchOfData ) {
			info.AddValue("key"+i, pair.Key);
			info.AddValue("value"+i, pair.Value);
			i++;
		}
	}
	*/
	
	#endregion
}
