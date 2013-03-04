using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof (HSimpleSprite))] 
public class HSimpleSpriteEditor : Editor
{

	
	// Use this for initialization
	public override void OnInspectorGUI ()
	{
		HSimpleSprite obj;

		obj = target as HSimpleSprite;

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
