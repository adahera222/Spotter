using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[RequireComponent (typeof (MeshCollider))]
[RequireComponent (typeof (MeshFilter))]
[RequireComponent (typeof (MeshRenderer))]


public class Grid : MonoBehaviour
{
	//Each grid square size
	public float m_CellSize = 0.0f;
	//Map dimensions
	public float m_Width = 0.0f;
	public float m_Height = 0.0f;
	//Mid point of map
	public Vector2 m_MidPoint = new Vector2(0.0f, 0.0f);
	//Z pos of grid
	public float m_MapZLayer = 0.0f;
	//Width of grid lines
	public float m_GridWidth = 0.1f;
	//List to hold mesh data
	List<Vector3> vertList = new List<Vector3>();
	List<Vector2> uvList = new List<Vector2>();
	List<int> triList = new List<int>();
	
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
	public void Rebuild()
	{
		//Set z layer
		Vector3 vPos =  transform.position;
		vPos.z = m_MapZLayer;
		transform.position = vPos;
		
		vertList.Clear();
		uvList.Clear();
		triList.Clear();
		
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		if(meshFilter == null)
		{
			Debug.LogError("MeshFilter not found!");
			return;
		}
		
		Mesh mesh = meshFilter.sharedMesh;
		if (mesh == null)
		{
			meshFilter.mesh = new Mesh();
			mesh = meshFilter.sharedMesh;
		}
		
		mesh.Clear();
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
			
			vertList.Add(v0);
			vertList.Add(v1);
			vertList.Add(v2);
			vertList.Add(v3);
			
			uvList.Add(new Vector2(0.0f, 0.0f));
			uvList.Add(new Vector2(1.0f, 0.0f));
			uvList.Add(new Vector2(1.0f, 1.0f));
			uvList.Add(new Vector2(0.0f, 1.0f));
			
			triList.Add(i); triList.Add(i+3); triList.Add(i+1);
			triList.Add(i+3); triList.Add(i+2); triList.Add(i+1);
			
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
			
			vertList.Add(v0);
			vertList.Add(v1);
			vertList.Add(v2);
			vertList.Add(v3);
			
			uvList.Add(new Vector2(0.0f, 0.0f));
			uvList.Add(new Vector2(1.0f, 0.0f));
			uvList.Add(new Vector2(1.0f, 1.0f));
			uvList.Add(new Vector2(0.0f, 1.0f));
			
			triList.Add(i); triList.Add(i+3); triList.Add(i+1);
			triList.Add(i+3); triList.Add(i+2); triList.Add(i+1);
			
			i+=4;
		}
		
		mesh.vertices = vertList.ToArray();
		mesh.triangles = triList.ToArray();
		mesh.uv1 = uvList.ToArray();
	
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		mesh.Optimize();
	}
	
}
