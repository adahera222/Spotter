using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof (HGridSprite))] 
public class HGridEditor : Editor 
{
	


	// Use this for initialization
	public override void OnInspectorGUI ()
	{
		HGridSprite obj = target as HGridSprite;

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