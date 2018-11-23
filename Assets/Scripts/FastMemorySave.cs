using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

public class FastMemorySave : MonoBehaviour {
	public SaveData saveData;
	public bool saveRandomData = true;
	public bool saveToFile = false;
	public int moduloCheck = 60;
	MemoryStream mStream;
	FileStream fStream;

	void Start() {
		LoadFromFile();
	}
	
	// Update is called once per frame
	void Update () {
		if ( saveRandomData ) {
			saveData.SetValue("random"+Random.Range(0,100), Random.Range(0,10000));
		}
		if ( Time.frameCount % moduloCheck == 0 ) {
			SaveToMemory();
		}
	}

	void OnApplicationQuit() {
		saveToFile = true;
		SaveToMemory();
	}

	void SaveToMemory() {
		mStream = new MemoryStream();
		try {
			//BinaryFormatter bf = new BinaryFormatter();
			//bf.Serialize(mStream, saveData);

			XmlSerializer xmls = new XmlSerializer(typeof(SaveData));
			xmls.Serialize(mStream, saveData);
			mStream.Flush();
		}
		catch ( System.Exception e ) {
			Debug.LogError(e.Message);
			Debug.LogError(e.StackTrace);
		}

		if ( saveToFile ) {
			PushToFile();
		}
	}

	void PushToFile() {
		fStream = new FileStream( Path.Combine( Application.streamingAssetsPath, "test.txt"), FileMode.Create );

		try {
			//write memorystream to file, by copying the entire buffer to the filestream
			//We use ToArray and not GetBuffer to only get actual data
			// otherwise we get a bunch of NULL data at the end
			byte[] bytes = mStream.ToArray();	
			fStream.Write(bytes, 0, bytes.Length);

			fStream.Flush();
			fStream.Close();
		}
		catch ( System.Exception e ) {
			Debug.LogError( e.Message );
		}
		finally {
			fStream.Dispose();
			mStream.Dispose();
		}
	}

	public void LoadFromFile() {
		string url = Path.Combine( Application.streamingAssetsPath, "test.txt");
		if ( !File.Exists(url)) {
			//initialize new save file
			Debug.Log("Creating new save data");
			saveData = new SaveData();
			return;
		}

		try {
			fStream = new FileStream( url , FileMode.Open );
			XmlSerializer xmls = new XmlSerializer(typeof(SaveData));
			saveData = (SaveData)xmls.Deserialize(fStream);

			//Not entirely sure why I need to call this myself
			// but probably because Unity only calls this when it handles deserialization automatically
			saveData.OnAfterDeserialize();	//copies data into dictionary
		}
		catch ( IOException e ) {
			Debug.Log(e.Message);
		}
		finally {
			//null if no file found, but we caught that one right at the start with File/Exists
			fStream.Close();
			fStream.Dispose();
		}

		Debug.Log("Loaded");
	}
}
