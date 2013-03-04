using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]

public enum MapOrientation
{
	Default = 0,
	NorthEast = -45,
	NorthWest = 45,
	SouthEast = -135,
	SouhtWest = 135,
}

public class MapManager : MonoBehaviour 
{
	public int m_MapWidth = 0;
	public int m_MapHeight = 0;
	public float m_GridWidth = 0.1f;

	public MapOrientation m_MapOrientation = MapOrientation.Default;
	private MapOrientation m_MapCurrentOrientation = MapOrientation.Default;

	private int m_CellSize = 1;
	private float m_DepthOffset = -0.02f;
	private Vector3 m_Origin = new Vector3(0.0f, 0.0f, 0.0f);
	//List of all grid positions and z offset used for point detection
	[HideInInspector]
	[SerializeField]
	private List<Vector3> m_GridPos = new List<Vector3>();
	//List of all grid positions and z offset rotated with map
	[HideInInspector]
	[SerializeField]
	private List<Vector3> m_GridPosRotated = new List<Vector3>();
	
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
		if(m_MapCurrentOrientation != m_MapOrientation)
		{
			//Reove old rotation
			int temp = -((int)m_MapCurrentOrientation);
			RotateMap(temp);
			m_MapCurrentOrientation = m_MapOrientation;
			//Rotate to new position
			temp = (int)m_MapCurrentOrientation;
			RotateMap(temp);
		}
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
		mapGrid.m_CellSize = m_CellSize;
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

		m_GridPos.Clear();
		m_GridPosRotated.Clear();
			
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
				m_GridPos.Add(new Vector3(posX, posY, posZ));
				m_GridPosRotated.Add(new Vector3(posX, posY, posZ));
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
		//Rotate point reverse of angle
		int angle = -((int)m_MapCurrentOrientation);
		Vector3 point = new Vector3(x, y, 0.0f);
		Vector3 oldDir = point - m_Origin;
		float newX = Mathf.Cos(angle*Mathf.Deg2Rad) * (oldDir.x) - Mathf.Sin(angle*Mathf.Deg2Rad) * (oldDir.y);   
        float newY = Mathf.Sin(angle*Mathf.Deg2Rad) * (oldDir.x) + Mathf.Cos(angle*Mathf.Deg2Rad) * (oldDir.y);     
		
		//Check if point intersects grid
		float halfCell = m_CellSize / 2.0f;
		//Simple iterate through list find grid cell object overlaps
		for (int i=0; i<m_GridPos.Count; i++)
		{
			Vector3 gridPos = m_GridPos[i];
			if(newX>(gridPos.x - halfCell) && newX<(gridPos.x + halfCell))
			{
				if(newY>(gridPos.y - halfCell) && newY<(gridPos.y + halfCell))
					return  m_GridPosRotated[i];
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
		for(int i=0; i < m_GridPosRotated.Count; i++)
		{
			Vector3 oldDir = m_GridPosRotated[i] - m_Origin;
			float newX = Mathf.Cos(angle*Mathf.Deg2Rad) * (oldDir.x) - Mathf.Sin(angle*Mathf.Deg2Rad) * (oldDir.y);   
        	float newY = Mathf.Sin(angle*Mathf.Deg2Rad) * (oldDir.x) + Mathf.Cos(angle*Mathf.Deg2Rad) * (oldDir.y);     
      		float newZ = oldDir.z; 
			m_GridPosRotated[i] = new Vector3(newX, newY, newZ);
		}
		
		//Rotate map sprite
		//m_Map.transform.RotateAround(m_Origin, new Vector3(0.0f, 0.0f, 1.0f), angle);
		//Rotate grid sprite
		//m_Grid.transform.RotateAround(m_Origin, new Vector3(0.0f, 0.0f, 1.0f), angle);
		m_Map.transform.Rotate(0.0f, 0.0f, angle);
		m_Grid.transform.Rotate(0.0f, 0.0f, angle);
	}
}
