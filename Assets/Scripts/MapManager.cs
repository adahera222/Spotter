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
	//List of all grid positions and z offset
	private List<Vector3> m_GridList = new List<Vector3>();
	
	//Z Depth/Layer of grid
	public DepthLayer m_MapDepthLayer = DepthLayer.Map;
	//Gameobjects for map and grid
	GameObject m_Map = null;
	GameObject m_Grid = null;
	HSprite mapSprite = null;
	HGridSprite mapGrid = null;
	
	//=================================
	// Use this for initialization
	//================================
	void Start ()
	{
		
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
		if(m_Map == null || m_Grid == null)
		{
		
			m_Map = new GameObject("Map Sprite");
		    mapSprite = m_Map.AddComponent<HSprite>();
		
			m_Grid = new GameObject("Map Grid");
		    mapGrid = m_Grid.AddComponent<HGridSprite>();
		}
		else
		{
			DestroyImmediate(m_Map);
			DestroyImmediate(m_Grid);
			
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
		gridLayer.SetDepthOffset(-0.1f);
		
		mapSprite.Rebuild();
		mapGrid.Rebuild();
		
	}
}
