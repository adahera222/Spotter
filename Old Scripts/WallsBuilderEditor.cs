using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof (WallsBuilder))] 
public class WallsBuilderEditor : Editor 
{
	WallsBuilder m_WallsBuilder;
	static int index = 0;
	
	private string[] options = new string[] {"Default", "East",
		"West", "South"};
	
	
	//==========================================
	// Update inspector gui
	//==========================================
	public override void OnInspectorGUI()
	{
		m_WallsBuilder = target as WallsBuilder;
		
		EditorGUILayout.BeginHorizontal();
			index = EditorGUILayout.Popup("View Side", index, options, EditorStyles.popup);
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Wall Object");
			m_WallsBuilder.m_WallObject = EditorGUILayout.ObjectField(m_WallsBuilder.m_WallObject, typeof(GameObject),
				false) as GameObject;
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		if(GUILayout.Button("Add Wall"))
			{	
				m_WallsBuilder.CreateWall();
			}
		GUILayout.EndHorizontal();
		
		EditorGUILayout.LabelField("========== Clear Data ============");
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("Clear Side"))
		{	
			m_WallsBuilder.ClearSide();
		}
		if(GUILayout.Button("Clear All"))
		{	
			m_WallsBuilder.ClearAll();
		}
		GUILayout.EndHorizontal();
		
		EditorGUILayout.LabelField("Save Walls");
		GUILayout.BeginHorizontal();
		m_WallsBuilder.m_SaveFilePath = GUILayout.TextArea(m_WallsBuilder.m_SaveFilePath);
		if(GUILayout.Button("Save Map"))
		{	
			m_WallsBuilder.SaveToXml(m_WallsBuilder.m_SaveFilePath);
		}
		GUILayout.EndHorizontal();
		
		ChangeView();
		
	}	
	
	//=============================================
	// Change view state
	//=============================================
	void ChangeView()
	{
		if(options[index] == "Default")
			m_WallsBuilder.m_ChangeState = ViewState.Default;
		else if(options[index] == "East")
			m_WallsBuilder.m_ChangeState = ViewState.East;
		else if(options[index] == "West")
			m_WallsBuilder.m_ChangeState = ViewState.West;
		else if(options[index] == "South")
			m_WallsBuilder.m_ChangeState = ViewState.South;
		
		//Check if state has changed
		if(m_WallsBuilder.m_ChangeState != m_WallsBuilder.m_CurrentState)
		{
			m_WallsBuilder.m_CurrentState = m_WallsBuilder.m_ChangeState ;
			m_WallsBuilder.ChangeState();
		}
	}
	
	
	
}
