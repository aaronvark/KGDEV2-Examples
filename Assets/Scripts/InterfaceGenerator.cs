using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;


#if UNITY_EDITOR
using UnityEditor;
using System.Reflection;

public class InterfaceGenerator : ScriptableWizard {
	
	List<System.Type> types;
	List<string> typeNames;
	string[] typeNamesArr;
	int selected = 0;
	string interfaceName;

	[MenuItem("Category/CreateInterface")]
	static void Open() {
		ScriptableWizard.DisplayWizard<InterfaceGenerator>("Interface Generator");
	}

	void OnEnable() {
		types = new List<System.Type>();
		typeNames = new List<string>();

		Assembly myAssembly = Assembly.GetExecutingAssembly();
		foreach( System.Type t in myAssembly.GetTypes()) {
			Debug.Log(t.ToString());
			types.Add(t);
			typeNames.Add(t.ToString());
		}

		typeNamesArr = typeNames.ToArray();
	}

	void OnGUI() {
		EditorGUI.BeginChangeCheck();
		selected = EditorGUILayout.Popup( selected, typeNamesArr );
		if ( EditorGUI.EndChangeCheck() ) {
			//set a default name when selection changes
			interfaceName =	"I"+typeNamesArr[selected].ToString();
		}

		GUILayout.BeginHorizontal();
		GUILayout.Label("Interface Name: ");
		interfaceName = GUILayout.TextField( interfaceName );
		GUILayout.EndHorizontal();

		if ( GUILayout.Button("Generate Interface") ) {
			InterfaceCoder.Generate( types[selected], interfaceName );
		}
	}
}

public static class InterfaceCoder {
	public static void Generate( System.Type T, string interfaceName ) {
		List<MethodInfo> publicMethods = new List<MethodInfo>();

		//parses public, non-static methods into list (only those defined by this type)
		foreach( MethodInfo mi in T.GetMethods() ) {
			if ( mi.IsPublic && !mi.IsStatic && mi.DeclaringType == T) {
				publicMethods.Add(mi);
			}
		}

		StringBuilder sb = new StringBuilder();

		//CONSTRUCT INTERFACE SCRIPT
		Header(sb);
		InterfaceOpener(sb, interfaceName);

		foreach( MethodInfo mi in publicMethods ) {
			sb.Append("\t");	//indent code inside class!
			MethodPrototype(sb, mi);
		}

		InterfaceCloser(sb);
		//FINISHED

		Debug.Log(sb.ToString());	//preview in console

		//Store in .cs file
		StreamWriter stream = null;
		try
		{
			string url = Path.Combine( Application.dataPath, "Scripts/Generated/" + interfaceName + ".cs");
			if ( File.Exists(url) ) {
				File.Delete(url);
				AssetDatabase.Refresh();
			}

			stream = File.CreateText(url);
			
			stream.Write( sb );
			stream.Flush();			
			
			AssetDatabase.Refresh();
		}
		catch ( System.Exception e ) {
			Debug.LogError( e.Message );
		}
		finally {
			if ( stream != null ) {
				stream.Close();
			}
		}
		
		//Imports and compiles the new interface
		AssetDatabase.Refresh();
	}

	public static void Header( StringBuilder sb ) {
		sb.AppendLine("using System.Collections;");
		sb.AppendLine("using System.Collections.Generic;");
		sb.AppendLine("using UnityEngine;");
		sb.AppendLine("");
	}

	public static void InterfaceOpener( StringBuilder sb, string className ) {
		sb.AppendLine("public interface "+className+" {");
	}

	public static void InterfaceCloser( StringBuilder sb ) {
		sb.AppendLine("}");
	}

	public static void MethodPrototype( StringBuilder sb, MethodInfo mi ) {

		//METHOD NAME
		//hack to avoid using System.Void in code, which is not allowed apparently
		if ( mi.ReturnType == typeof(void)) {
			sb.Append("void");
		}
		else {
			sb.Append(mi.ReturnType.ToString());
		}
		sb.Append(" ");
		sb.Append(mi.Name);
		sb.Append("( ");

		//PARAMETERS
		ParameterInfo[] paramList = mi.GetParameters();
		for( int i = 0; i < paramList.Length; ++i ) {
			sb.Append( paramList[i].ParameterType.ToString() );
			sb.Append( " " );
			sb.Append( paramList[i].Name );
			if ( i < paramList.Length - 1 ) {
				sb.Append(", ");
			}
		}
		sb.Append( ");\n" );
	}
}
#endif