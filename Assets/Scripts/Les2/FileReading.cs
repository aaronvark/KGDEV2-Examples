using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Reflection;

//simpele class om wat data in op te slaan
[System.Serializable] //<- tells compiler this can be serialized
public class SerializableData {
	public string name;			//<- these are both "atomic" datatypes, so this
	public int someIntValue;	//		can always be serialized
}

public class FileReading : MonoBehaviour {
	//Can't serialize the Data class due to Object reference inside of it
	//Unity handles serialization differently, so it can store these kinds of objects
	public Data myData;

	//This class serializes properly
	public SerializableData otherData;

	//for the Reflection code below
	//this is a MonoBehaviour so it "hides" its type, which we can then ask for and work with
	public MonoBehaviour myClassObject;

	// Use this for initialization
	void Start () {
		//Functies die van alles doen met het inladen en wegschrijven van bestanden
		//Probeer ze eens uit, en kijk wat er in de bestandjes terecht komt, of wanneer het mis gaat!
		//CreateFileNew ();
		//LoadFileNew ();

		//CreateFile ();
		//LoadFile ();

		//WriteFileBinary ();
		ReadFileBinary();

		//WriteFileXml ();
		//ReadFileXml();

		//Reflection
		//Get the type of the myClassObject variable
		System.Type t = myClassObject.GetType();
		//Get all methods that are: non-public (protected & private) instance (non-static) and declared at this level (not inherited)
		MethodInfo[] methods = t.GetMethods (BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
		//loop through them
		foreach (MethodInfo method in methods) {
			//find one with no parameters
			if (method.GetParameters ().Length == 0) {
				//invoke that method, on the object, with an empty parameter array
				method.Invoke (myClassObject, new object[]{ });
			}
		}
	}

	void WriteFileBinary() {
		//create a formatter so we can serialize things to binary
		BinaryFormatter bf = new BinaryFormatter ();

		//combine lets you prevent errors with / or \ on different platforms
		//and StreamingAssets is the place to store things "as is"
		//in Resources things are "compiled" (example: mp4 becomes MovieTexture)
		string pathUrl = Path.Combine (Application.streamingAssetsPath, "binaryFile.txt");
		//open a filestream to this location, with the Create mode
		FileStream fStream = new FileStream (pathUrl, FileMode.Create);

		//Tell the formatter to serialize some data, and write it to fStream
		bf.Serialize (fStream, otherData);

		//flush and close the stream to finish writing the file
		fStream.Flush ();
		fStream.Close();
	}

	void ReadFileBinary() {
		//Create a formatter so we can deserialize some binary data
		BinaryFormatter bf = new BinaryFormatter ();

		//Combine again to simplify the process of creating a valid path
		string pathUrl = Path.Combine (Application.streamingAssetsPath, "binaryFile.txt");
		//start with a null filestream
		FileStream fStream = null;

		//try some code
		try {
			fStream = new FileStream (pathUrl, FileMode.Open);
			otherData = (SerializableData) bf.Deserialize(fStream);
		} 
		//if it causes a problem, catch the exception so we can continue "gracefully"
		catch ( System.Exception e ) {
			//print the message
			Debug.LogError (e.Message);
		}
		//this happens even if a problem occurred
		finally {
			//make sure that we check if the stream was created properly before closing it
			if (fStream != null) {
				fStream.Close ();
			}
		}
	}

	void WriteFileXml() {
		XmlSerializer bf = new XmlSerializer (typeof(SerializableData));

		string pathUrl = Path.Combine (Application.streamingAssetsPath, "xmlFile.txt");
		FileStream fStream = new FileStream (pathUrl, FileMode.Create);

		bf.Serialize (fStream, otherData);

		fStream.Flush ();
		fStream.Close ();
	}

	void ReadFileXml() {

		XmlSerializer bf = new XmlSerializer (typeof(SerializableData));

		string pathUrl = Path.Combine (Application.streamingAssetsPath, "xmlFile.txt");
		FileStream fStream = new FileStream (pathUrl, FileMode.Open);

		otherData = (SerializableData) bf.Deserialize(fStream);

		fStream.Close ();
	}

	void CreateFileNew() {
		StreamWriter sw = new StreamWriter (Path.Combine (Application.streamingAssetsPath, "bestandje2.txt"));

		//dit flushed na elke write
		//sw.AutoFlush = true;
		sw.WriteLine ("Dit is een regel");

		//deze twee staan op 1 regel
		sw.Write ("Dit niet");
		sw.Write ("En dat zie je");

		sw.Flush ();
		sw.Close ();

		//this only works in the editor, so I have commented it out
		//you can use preprocessor directives to make this safe (#if UNITY_EDITOR, #endif)
		//UnityEditor.AssetDatabase.Refresh ();
	}

	void LoadFileNew() {
		StreamReader sr = new StreamReader (Path.Combine (Application.streamingAssetsPath, "bestandje2.txt"));

		while (!sr.EndOfStream) {
			Debug.Log (sr.ReadLine ());
		}

		sr.Close ();
	}



	void CreateFile() {
		string pathUrl = Path.Combine (Application.streamingAssetsPath, "bestandje.txt");

		//open
		StreamWriter stream = new StreamWriter (pathUrl);
		stream.WriteLine ("Regeltje 1");
		stream.WriteLine ("Regeltje 2");
		stream.WriteLine ("Regeltje 3");
		stream.WriteLine ("Regeltje 4");

		//flush (als een stream sluit voordat hij flushed, kan het zijn dat niet alles is weg geschreven!
		stream.Flush ();

		//close
		stream.Close ();
	}

	void LoadFile() {
		string pathUrl = Path.Combine (Application.streamingAssetsPath, "bestandje.txt");

		StreamReader stream = null;

		//probeert de code in het blok uit te voeren, maar het mag mis gaan
		try
		{
			stream = new StreamReader (pathUrl);

			while (!stream.EndOfStream) {
				print (stream.ReadLine ());
			}
		}
		//vang een error af (zodat je er nog iets mee kan doen zonder dat je game crashed)
		//je kan hier ook specifiek zijn in wat voor errors je opvangt
		catch ( System.Exception exception ) {
			print (exception.Message);
		}
		//dit gebeurt ongeacht of het goed of fout gaat
		finally {
			if (stream != null) {
				stream.Close ();
			}
		}
	}
}
