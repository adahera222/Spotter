using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor (typeof (HLayer))] 
public class HLayerEditor : Editor 
{

	// Use this for initialization
	public override void OnInspectorGUI ()
	{
		HLayer obj;

		obj = target as HLayer;

		if (obj == null)
		{
			return;
		}
		
		EditorGUILayout.LabelField("Depth Layer");
		base.DrawDefaultInspector();
		
		EditorGUILayout.LabelField("Set depth offset");
		EditorGUILayout.BeginHorizontal ();
			if (GUILayout.Button("<"))
			{
				obj.m_ZOffset -= 0.02f;
			}
			obj.m_ZOffset = EditorGUILayout.FloatField(obj.m_ZOffset);
			if (GUILayout.Button(">"))
			{
				obj.m_ZOffset += 0.02f;
			} 
		EditorGUILayout.EndHorizontal ();
		
		
		EditorGUILayout.BeginHorizontal ();
		if (GUILayout.Button("set Layer"))
		{
			obj.SetLayer(obj.m_Layer);
		}
		EditorGUILayout.EndHorizontal ();
	}
	
	
	
	
}