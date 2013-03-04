using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[RequireComponent (typeof (MeshCollider))]
[RequireComponent (typeof (MeshFilter))]
[RequireComponent (typeof (MeshRenderer))]
[RequireComponent (typeof (HLayer))]

public class HGridSprite : HSprite
{
	//Width of grid lines
	public float m_GridWidth = 0.1f;
	//Main color of grid
	public Color m_MainColor = Color.grey;
	//Each grid square size
	public float m_CellSize = 1.0f;
	//Midpoint of grid is origin
	private Vector3 m_MidPoint = new Vector3(0.0f, 0.0f, 0.0f);
	//List to contain vertex colors
	[HideInInspector]
	[SerializeField]
	private List<Color> m_VertColors = new List<Color>();
	
	//=====================================
	// Use this for initialization
	//=====================================
	void Start ()
	{
		
	}
	
	//=======================================
	// Update is called once per frame
	//=======================================
	void Update () 
	{
		
	}
	
	//========================================
	// Rebuild mesh data
	//========================================
	override public void Rebuild()
	{
		
		vertList.Clear();
		uvList.Clear();
		triList.Clear();
		m_VertColors.Clear();
		
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		if(meshRenderer == null)
		{
			Debug.LogError("MeshRender not found!");
			return;
		}
		meshRenderer.material.shader = Shader.Find("Custom/GridShader");
		
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		if(meshFilter == null)
		{
			Debug.LogError("MeshFilter not found!");
			return;
		}
		
		m_Mesh = meshFilter.sharedMesh;
		if (m_Mesh == null)
		{
			meshFilter.mesh = new Mesh();
			m_Mesh = meshFilter.sharedMesh;
		}
		
		m_Mesh.Clear();
	
		//================================
		float halfGrid = m_GridWidth / 2.0f;
		float halfW = m_Width / 2.0f;
		float halfH = m_Height / 2.0f;
		
		float startX = m_MidPoint.x - halfW;
		float endX = m_MidPoint.x + halfW;
		float startY = m_MidPoint.y - halfH;
		float endY = m_MidPoint.y + halfH;
		
		//Horizontal lines
		int i=0;
		for(float y=startY; y<=endY; y+=m_CellSize)
		{
			float x0 = startX - halfGrid;
			float x1 = endX + halfGrid;
			float y0 = y - halfGrid;
			float y1 = y + halfGrid;
			
			Vector3 v0 = new Vector3(x0, y0, 0.0f);
			Vector3 v1 = new Vector3(x1, y0, 0.0f);
			Vector3 v2 = new Vector3(x1, y1, 0.0f);
			Vector3 v3 = new Vector3(x0, y1, 0.0f);
			//Verts
			vertList.Add(v0);
			vertList.Add(v1);
			vertList.Add(v2);
			vertList.Add(v3);
			//UVs
			uvList.Add(new Vector2(0.0f, 0.0f));
			uvList.Add(new Vector2(1.0f, 0.0f));
			uvList.Add(new Vector2(1.0f, 1.0f));
			uvList.Add(new Vector2(0.0f, 1.0f));
			//Triangles
			triList.Add(i); triList.Add(i+3); triList.Add(i+1);
			triList.Add(i+3); triList.Add(i+2); triList.Add(i+1);
			//Color
			m_VertColors.Add(m_MainColor);
			m_VertColors.Add(m_MainColor);
			m_VertColors.Add(m_MainColor);
			m_VertColors.Add(m_MainColor);
			
			i+=4;
		}
		//Vertical lines
		for(float x=startX; x<=endX; x+=m_CellSize)
		{
			float x0 = x - halfGrid;
			float x1 = x + halfGrid;
			float y0 = startY - halfGrid;
			float y1 = endY + halfGrid;
			
			Vector3 v0 = new Vector3(x0, y0, 0.0f);
			Vector3 v1 = new Vector3(x1, y0, 0.0f);
			Vector3 v2 = new Vector3(x1, y1, 0.0f);
			Vector3 v3 = new Vector3(x0, y1, 0.0f);
			//Verts
			vertList.Add(v0);
			vertList.Add(v1);
			vertList.Add(v2);
			vertList.Add(v3);
			//UVs
			uvList.Add(new Vector2(0.0f, 0.0f));
			uvList.Add(new Vector2(1.0f, 0.0f));
			uvList.Add(new Vector2(1.0f, 1.0f));
			uvList.Add(new Vector2(0.0f, 1.0f));
			//Triangles
			triList.Add(i); triList.Add(i+3); triList.Add(i+1);
			triList.Add(i+3); triList.Add(i+2); triList.Add(i+1);
			//Color
			m_VertColors.Add(m_MainColor);
			m_VertColors.Add(m_MainColor);
			m_VertColors.Add(m_MainColor);
			m_VertColors.Add(m_MainColor);
			
			i+=4;
		}
		
		m_Mesh.vertices = vertList.ToArray();
		m_Mesh.triangles = triList.ToArray();
		m_Mesh.uv1 = uvList.ToArray();
		m_Mesh.colors = m_VertColors.ToArray();
		
	
		m_Mesh.RecalculateNormals();
		m_Mesh.RecalculateBounds();
		m_Mesh.Optimize();
	}
	
}
