using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof (Sprite))] 
public class SpriteEditor : Editor
{
	
	[MenuItem ("GameObject/Create Other/Sprite")]
	//==============================================
	//Create custom game object
	//==============================================
	static void Create()
	{
		//float m_WallLength = 5.0f;
		//float m_WallHeight = 5.0f;
		
		GameObject gameObject = new GameObject("Sprite");
		Sprite s = gameObject.AddComponent<Sprite>();
		s.Rebuild();
	
	}

	// Use this for initialization
	public override void OnInspectorGUI ()
	{
		Sprite obj;

		obj = target as Sprite;

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
