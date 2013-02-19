using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(GridBuilder))]

public class GridBuilderEditor : Editor
{
	GridBuilder m_GridBuilder;
	//==========================================
	// Update inspector gui
	//==========================================
	public override void OnInspectorGUI()
	{
		m_GridBuilder = target as GridBuilder;
		
		///////////////////////////////////////////////////////////////////////////////
		//Orientation
		EditorGUILayout.LabelField("Init Data ===================================================");
		GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Grid Cell Size");
			m_GridBuilder.m_CellSize = EditorGUILayout.IntField(m_GridBuilder.m_CellSize);
			EditorGUILayout.LabelField("Grid Cell Object");
			m_GridBuilder.m_CellObject = EditorGUILayout.ObjectField(m_GridBuilder.m_CellObject, typeof(GameObject),
				false) as GameObject;
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
			if(GUILayout.Button("Clear Map"))
			{	
				m_GridBuilder.ClearMap();
			}
		GUILayout.EndHorizontal();
		
		EditorGUILayout.LabelField("Build Map ===================================================");
		GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Number of Cells");
			m_GridBuilder.m_NumOfCells = EditorGUILayout.IntField(m_GridBuilder.m_NumOfCells);
			if(GUILayout.Button("Add Column"))
			{	
				//Remove the old rotation
				m_GridBuilder.RotateGrid(-m_GridBuilder.m_CurrentAngle, m_GridBuilder.m_Origin);
				m_GridBuilder.m_CurrentAngle = 0.0f;
				m_GridBuilder.m_NoRot = true;
				m_GridBuilder.m_Default = false;
				m_GridBuilder.m_East = false;
				m_GridBuilder.m_West = false;
				m_GridBuilder.m_South = false;
				m_GridBuilder.AddGridColumn(m_GridBuilder.m_NumOfCells);	
			}
		GUILayout.EndHorizontal();
		
		EditorGUILayout.LabelField("Save Map ===================================================");
		GUILayout.BeginHorizontal();
		m_GridBuilder.m_SavefilePath = GUILayout.TextArea(m_GridBuilder.m_SavefilePath);
		if(GUILayout.Button("Save Map"))
		{	
			//Remove the old rotation
			m_GridBuilder.RotateGrid(-m_GridBuilder.m_CurrentAngle, m_GridBuilder.m_Origin);
			m_GridBuilder.m_CurrentAngle = 0.0f;
			m_GridBuilder.m_NoRot = true;
			m_GridBuilder.m_Default = false;
			m_GridBuilder.m_East = false;
			m_GridBuilder.m_West = false;
			m_GridBuilder.m_South = false;
			m_GridBuilder.SaveGridToFile(m_GridBuilder.m_SavefilePath);
		}
		GUILayout.EndHorizontal();
		
		///////////////////////////////////////////////////////////////////////////////
		//Orientation
		EditorGUILayout.LabelField("Angle ======================================================");
		GUILayout.BeginHorizontal();
			
			GUILayout.BeginVertical();
				EditorGUILayout.LabelField("None");
				m_GridBuilder.m_NoRot = EditorGUILayout.Toggle(m_GridBuilder.m_NoRot);	
			GUILayout.EndVertical();
			GUILayout.BeginVertical();
				EditorGUILayout.LabelField("Default");
				m_GridBuilder.m_Default = EditorGUILayout.Toggle(m_GridBuilder.m_Default);
			GUILayout.EndVertical();
			GUILayout.BeginVertical();
				EditorGUILayout.LabelField("West");
				m_GridBuilder.m_West = EditorGUILayout.Toggle(m_GridBuilder.m_West);
			GUILayout.EndVertical();
			GUILayout.BeginVertical();
				EditorGUILayout.LabelField("East");
				m_GridBuilder.m_East = EditorGUILayout.Toggle(m_GridBuilder.m_East);
			GUILayout.EndVertical();
			GUILayout.BeginVertical();
				EditorGUILayout.LabelField("South");
				m_GridBuilder.m_South = EditorGUILayout.Toggle(m_GridBuilder.m_South);
			GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		//Rotate angle
		if(m_GridBuilder.m_NoRot && m_GridBuilder.m_CurrentAngle != 0.0f)
		{
			//Remove the old rotation
			m_GridBuilder.RotateGrid(-m_GridBuilder.m_CurrentAngle, m_GridBuilder.m_Origin);
			m_GridBuilder.m_CurrentAngle = 0.0f;
			
			m_GridBuilder.m_Default = false;
			m_GridBuilder.m_East = false;
			m_GridBuilder.m_West = false;
			m_GridBuilder.m_South = false;
		}
		if(m_GridBuilder.m_Default && m_GridBuilder.m_CurrentAngle != -45.0f)
		{
			//Remove the old rotation
			m_GridBuilder.RotateGrid(-m_GridBuilder.m_CurrentAngle, m_GridBuilder.m_Origin);
			m_GridBuilder.m_CurrentAngle = -45.0f;
			m_GridBuilder.RotateGrid(m_GridBuilder.m_CurrentAngle, m_GridBuilder.m_Origin);
			
			m_GridBuilder.m_East = false;
			m_GridBuilder.m_West = false;
			m_GridBuilder.m_NoRot = false;
			m_GridBuilder.m_South = false;
		}
		if(m_GridBuilder.m_West && m_GridBuilder.m_CurrentAngle != 45.0f)
		{
			//Remove the old rotation
			m_GridBuilder.RotateGrid(-m_GridBuilder.m_CurrentAngle, m_GridBuilder.m_Origin);
			m_GridBuilder.m_CurrentAngle = 45.0f;
			m_GridBuilder.RotateGrid(m_GridBuilder.m_CurrentAngle, m_GridBuilder.m_Origin);
			
			m_GridBuilder.m_Default = false;
			m_GridBuilder.m_East = false;
			m_GridBuilder.m_NoRot = false;
			m_GridBuilder.m_South = false;
		}
		if(m_GridBuilder.m_East && m_GridBuilder.m_CurrentAngle != -135.0f)
		{
			//Remove the old rotation
			m_GridBuilder.RotateGrid(-m_GridBuilder.m_CurrentAngle, m_GridBuilder.m_Origin);
			m_GridBuilder.m_CurrentAngle = -135.0f;
			m_GridBuilder.RotateGrid(m_GridBuilder.m_CurrentAngle, m_GridBuilder.m_Origin);
			
			m_GridBuilder.m_Default = false;
			m_GridBuilder.m_West = false;
			m_GridBuilder.m_NoRot = false;
			m_GridBuilder.m_South = false;
		}
		if(m_GridBuilder.m_South && m_GridBuilder.m_CurrentAngle != 135.0f)
		{
			//Remove the old rotation
			m_GridBuilder.RotateGrid(-m_GridBuilder.m_CurrentAngle, m_GridBuilder.m_Origin);
			m_GridBuilder.m_CurrentAngle = 135.0f;
			m_GridBuilder.RotateGrid(m_GridBuilder.m_CurrentAngle, m_GridBuilder.m_Origin);
			
			m_GridBuilder.m_Default = false;
			m_GridBuilder.m_East = false;
			m_GridBuilder.m_West = false;
			m_GridBuilder.m_NoRot = false;
		}
		//////////////////////////////////////////////////////////////////////////////////////////
	}
	
	//===========================================
	// Update scene gui
	//=============================================
	public void OnSceneGUI()
	{
		
	}
}
