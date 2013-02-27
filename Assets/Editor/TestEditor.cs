using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof (TestColor))] 
public class TestEditor : Editor
{
	/*
	[MenuItem ("GameObject/Create Other/HSprite")]
	//==============================================
	//Create custom game object
	//==============================================
	static void Create()
	{
		//float m_WallLength = 5.0f;
		//float m_WallHeight = 5.0f;
		
		GameObject gameObject = new GameObject("HSprite");
		Sprite s = gameObject.AddComponent<HSprite>();
		s.Rebuild();
	
	}
	 */
	
	// Use this for initialization
	public override void OnInspectorGUI ()
	{
		TestColor obj;

		obj = target as TestColor;

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
