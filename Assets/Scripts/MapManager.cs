using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class MapManager : MonoBehaviour 
{
	public int m_MapWidth = 0;
	public int m_MapHeight = 0;
	public float m_GridWidth = 0.1f;
	
	private int m_CellSize = 1;
	private float m_DepthOffset = -0.02f;
	private Vector3 m_Origin = new Vector3(0.0f, 0.0f, 0.0f);
	//List of all grid positions and z offset
	[HideInInspector]
	[SerializeField]
	private List<Vector3> m_GridList = new List<Vector3>();
	
	//Z Depth/Layer of grid
	public DepthLayer m_MapDepthLayer = DepthLayer.Map;
	//Gameobjects for map and grid
	[HideInInspector]
	[SerializeField]
	private GameObject m_Map = null;
	[HideInInspector]
	[SerializeField]
	private GameObject m_Grid = null;
	[HideInInspector]
	[SerializeField]
	private HSprite mapSprite = null;
	[HideInInspector]
	[SerializeField]
	private HGridSprite mapGrid = null;
	
	//=================================
	// Use this for initialization
	//================================
	void Start ()
	{
		//RotateMap(-45.0f);
	}
	
	//===================================
	// Update is called once per frame
	//===================================
	void Update () 
	{
	
	}
	
	//=====================================
	//Rebuild map
	//=====================================
	public void Rebuild()
	{
		//Save the position of each grid
		CalculateGridData();
		
		if(m_Map == null || m_Grid == null)
		{
		
			m_Map = new GameObject("Map Sprite");
		    mapSprite = m_Map.AddComponent<HSprite>();
		
			m_Grid = new GameObject("Map Grid");
		    mapGrid = m_Grid.AddComponent<HGridSprite>();
		}
	
		if(mapSprite == null)
		{
			Debug.LogError("Map sprite not found!");
			return;
		}
		if(mapGrid == null)
		{
			Debug.LogError("Map grid not found!");
			return;
		}	
		
		HLayer mapLayer = m_Map.GetComponent<HLayer>();
		HLayer gridLayer = m_Grid.GetComponent<HLayer>();
		
		if(mapLayer == null || gridLayer == null)
		{
			Debug.LogError("HLayer not found!");
			return;
		}
		
		mapSprite.m_Width = m_MapWidth;
		mapSprite.m_Height = m_MapHeight;
		mapLayer.SetLayer(m_MapDepthLayer);
		
		mapGrid.m_Width = m_MapWidth;
		mapGrid.m_Height = m_MapHeight;
		mapGrid.m_GridWidth = m_GridWidth;
		gridLayer.SetLayer(m_MapDepthLayer);
		gridLayer.SetDepthOffset(-0.01f);
		
		mapSprite.Rebuild();
		mapGrid.Rebuild();
		
			
	}
	
	//=====================================
	// Calculate the grid pos data
	//=====================================
	void CalculateGridData()
	{

		m_GridList.Clear();
			
		float halfW = m_MapWidth / 2.0f;
		float halfH = m_MapHeight / 2.0f;
		float halfCell = m_CellSize / 2.0f;
		
		float startX = -halfW; float endX = halfW;
		float startY = halfH; float endY = -halfH;
		
		float posZ = 0.0f;
		
		for(float x=startX; x<= (endX - m_CellSize); x+= m_CellSize)
		{
			//We want the center position of each square
			float posX = x + halfCell;
			for(float y=(startY - m_CellSize); y>= endY; y-=m_CellSize)
			{
				float posY = y + halfCell;
				//Add pos data
				m_GridList.Add(new Vector3(posX, posY, posZ));
				posZ += m_DepthOffset;
				
			}
		}
		//Debug.LogError(m_GridList.Count.ToString());
	}
	
	//==============================================
	//	Get grid depth value
	//==============================================
	public Vector3 GetNearestSquare(float x, float y)
	{
		float halfCell = m_CellSize / 2.0f;
		//Simple iterate through list find grid cell object overlaps
		for (int i=0; i<m_GridList.Count; i++)
		{
			Vector3 gridPos = m_GridList[i];
			if(x>(gridPos.x - halfCell) && x<(gridPos.x + halfCell))
			{
				if(y>(gridPos.y - halfCell) && y<(gridPos.y + halfCell))
					return gridPos;
			}
		}
		
		return new Vector3(0.0f, 0.0f, 0.0f);
	}
	
	//===============================================
	// Rotate everthing thats part of the map
	//===============================================
	public void RotateMap(float angle)
	{
		//Rotate Grid Coordinates
		for(int i=0; i < m_GridList.Count; i++)
		{
			Vector3 oldDir = m_GridList[i] - m_Origin;
			float newX = Mathf.Cos(angle*Mathf.Deg2Rad) * (oldDir.x) - Mathf.Sin(angle*Mathf.Deg2Rad) * (oldDir.y);   
        	float newY = Mathf.Sin(angle*Mathf.Deg2Rad) * (oldDir.x) + Mathf.Cos(angle*Mathf.Deg2Rad) * (oldDir.y);     
      		float newZ = oldDir.z; 
			m_GridList[i] = new Vector3(newX, newY, newZ);
		}
		
		//Rotate map sprite
		//m_Map.transform.RotateAround(m_Origin, new Vector3(0.0f, 0.0f, 1.0f), angle);
		//Rotate grid sprite
		//m_Grid.transform.RotateAround(m_Origin, new Vector3(0.0f, 0.0f, 1.0f), angle);
		m_Map.transform.Rotate(0.0f, 0.0f, angle);
		m_Grid.transform.Rotate(0.0f, 0.0f, angle);
	}
}
