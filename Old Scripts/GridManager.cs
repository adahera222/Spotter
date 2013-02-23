using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public enum ViewState
{
	Default,
	East,
	West,
	South,
}

//Manages data associated with the grid map
public class GridManager : MonoBehaviour
{
	public string m_MapFilePath = null;
	public GameObject m_GridCell = null;
	//Container of Grid cells as Unity GameObjects
	private List<GameObject> m_GridList = new List<GameObject>();
	private int m_NumberOfCells = 0;
	private int m_NumOfColumns = 0;
	
	private float m_CellSize = 1;
	private Vector3 m_Origin = new Vector3(0.0f, 0.0f, 1.0f);
	
	private bool m_IsInitialized = false;
	
	private ViewState m_CurrentState;
	private float m_ViewAngle = 0.0f;
	
	//=================================
	// Use this for initialization
	//===================================
	void Start () 
	{
		if(m_MapFilePath != null)
		{
			//Load map Data
			LoadMapFromFile(m_MapFilePath);
			//Set angle to default
			m_CurrentState = ViewState.Default;
			
		}
	}
	
	//==========================================
	// Update is called once per frame
	//============================================
	void Update () 
	{
		switch (m_CurrentState)
		{
			case ViewState.Default:
			{
				if(m_ViewAngle != -45.0f)
				{
					//Remove any rotation
					RotateGrid(-m_ViewAngle, m_Origin);
					//Rotate 
					m_ViewAngle = -45.0f;
					RotateGrid(m_ViewAngle, m_Origin);
				}
			break;
			}
			case ViewState.East:
			{
				if(m_ViewAngle != -135.0f)
				{
					//Remove any rotation
					RotateGrid(-m_ViewAngle, m_Origin);
					//Rotate 
					m_ViewAngle = -135.0f;
					RotateGrid(m_ViewAngle, m_Origin);
				}
			break;
			}
			case ViewState.West:
			{
				if(m_ViewAngle != 45.0f)
				{
					//Remove any rotation
					RotateGrid(-m_ViewAngle, m_Origin);
					//Rotate 
					m_ViewAngle = 45.0f;
					RotateGrid(m_ViewAngle, m_Origin);
				}
			break;
			}
			case ViewState.South:
			{
				if(m_ViewAngle != 135.0f)
				{
					//Remove any rotation
					RotateGrid(-m_ViewAngle, m_Origin);
					//Rotate 
					m_ViewAngle = 135.0f;
					RotateGrid(m_ViewAngle, m_Origin);
				}
			break;
			}	
		}
		
	}
	
	
	//=============================================
	//Load map from xml file
	//==============================================
	void LoadMapFromFile(string filePath)
	{
		
		XmlDocument doc = new XmlDocument();
		doc.Load(filePath);
		
		XmlNodeList eRootList = doc.GetElementsByTagName("Root");
		XmlElement eRoot = (XmlElement)eRootList.Item(0);
		string type = eRoot.GetAttribute("Type");
		
			//This is an actual map file
		if(type == "MapFile")
		{
			//Get Origin
			XmlNodeList olist = eRoot.GetElementsByTagName("Origin");
			XmlElement eOrigin = (XmlElement)olist.Item(0);
			float x = float.Parse(eOrigin.GetAttribute("X"));
			float y = float.Parse(eOrigin.GetAttribute("Y"));
			float z = float.Parse(eOrigin.GetAttribute("Z"));
			//Set the grid origin
			m_Origin.x = x; m_Origin.y = y; m_Origin.z = z;
			
			//Get cell size
			XmlNodeList slist = eRoot.GetElementsByTagName("Size");
			XmlElement eSize = (XmlElement)slist.Item(0);
			float s = float.Parse(eSize.GetAttribute("Size"));
			//Set the grid cell size
			m_CellSize = s;
			
			//Get all cell positions
			XmlNodeList clist = eRoot.GetElementsByTagName("Cell");
			XmlElement eCell = null;
			for(int i=0; i<clist.Count; i++)
			{
				eCell = (XmlElement)clist.Item(i);
				float cx = float.Parse(eCell.GetAttribute("X"));
				float cy = float.Parse(eCell.GetAttribute("Y"));
				float cz = float.Parse(eCell.GetAttribute("Z"));
				
				//Create Grid Cell
				m_GridList.Add(Instantiate(m_GridCell, new Vector3(cx, cy, cz), Quaternion.identity) as GameObject);
			}
			//Scale grid cells
			ScaleGrid(m_CellSize);
			
		}
	}
	
	//==============================================================
	// Scale entire grid
	//=================================================================
	private void ScaleGrid(float scale)
	{
		Vector3 vScale = new Vector3(scale, scale, scale);
		for (int i=0; i<m_GridList.Count; i++)
		{
			GameObject temp = m_GridList[i];
			temp.transform.localScale = vScale;
		}
	}
	
	//========================================================
	// Rotate entire grid
	//========================================================
	private void RotateGrid(float angle, Vector3 origin)
	{
		for (int i=0; i<m_GridList.Count; i++)
		{
			GameObject temp = m_GridList[i];
			temp.transform.RotateAround(origin, new Vector3(0.0f, 0.0f, 1.0f), angle);
		}
	}
	
	//==========================================
	// Change the visible side
	//============================================
	public void SetActiveSide(ViewState viewState)
	{
		m_CurrentState = viewState;
	}
	
}
