using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor (typeof (MapManager))] 
public class MapEditor : Editor 
{
	
	// Use this for initialization
	public override void OnInspectorGUI ()
	{
		MapManager obj;

		obj = target as MapManager;

		if (obj == null)
		{
			return;
		}

		base.DrawDefaultInspector();
		EditorGUILayout.BeginHorizontal ();

		// Rebuild mesh when user click the Rebuild button
		if (GUILayout.Button("Rebuild")){
			obj.Rebuild();
		}
		EditorGUILayout.EndHorizontal ();
	}
	
	
}