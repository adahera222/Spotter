using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof (IsoWall))] 
public class IsoWallEditor : Editor 
{
	[MenuItem ("GameObject/Create Other/IsoWall")]
	static void Create()
	{
		//float m_WallLength = 5.0f;
		//float m_WallHeight = 5.0f;
		
		GameObject gameObject = new GameObject("IsoWall");
		IsoWall s = gameObject.AddComponent<IsoWall>();
	//	MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
	//	meshFilter.mesh = new Mesh();
		
	
	}

	// Use this for initialization
	public override void OnInspectorGUI ()
	{
		IsoWall obj;

		obj = target as IsoWall;

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
