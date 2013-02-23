using UnityEngine;
using System.Collections;

//[RequireComponent (typeof (MeshCollider))]
[RequireComponent (typeof (MeshFilter))]
[RequireComponent (typeof (MeshRenderer))]

public class Sprite : MonoBehaviour 
{
	//Sprite placed with 0,0 as middle
	public float m_Width = 1.0f;
	public float m_Height = 1.0f;
	// Z-axis position
	public float m_ZLayer = 1.0f;
	
	private Mesh m_Mesh = null;
	
	//======================================
	// Use this for initialization
	//======================================
	void Start () 
	{
	
	}
	
	//=========================================
	// Update is called once per frame
	//=========================================
	void Update () 
	{
		
	}
	
	//===========================================
	// Rebuild Sprite mesh
	//===========================================
	public void Rebuild()
	{
		//Set z layer
		Vector3 vPos =  transform.position;
		vPos.z = m_ZLayer;
		transform.position = vPos;
		
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		if(meshFilter == null)
		{
			Debug.LogError("MeshFilter not found!");
			return;
		}
		
		float halfW = m_Width / 2.0f;
		float halfH = m_Height / 2.0f;
		
		Vector3 p0 = new Vector3(0.0f - halfW, 0.0f - halfH, 0.0f);
		Vector3 p1 = new Vector3(0.0f + halfW, 0.0f - halfH,0.0f);
		Vector3 p2 = new Vector3(0.0f + halfW, 0.0f + halfH, 0.0f);
		Vector3 p3 = new Vector3(0.0f - halfW, 0.0f + halfH, 0.0f);
		
		m_Mesh = meshFilter.sharedMesh;
		if (m_Mesh == null)
		{
			meshFilter.mesh = new Mesh();
			m_Mesh = meshFilter.sharedMesh;
		}
		
		m_Mesh.Clear();
		
		m_Mesh.vertices = new Vector3[]{p0,p1,p2,p3};
		m_Mesh.triangles = new int[]{
				0,3,1,
				3,2,1,
			};
		// basically just assigns a corner of the texture to each vertex
			m_Mesh.uv = new Vector2[]{
				new Vector2(0,0),
				new Vector2(1,0),
				new Vector2(1,1),
				new Vector2(0,1),
			};
		
		m_Mesh.RecalculateNormals();
		m_Mesh.RecalculateBounds();
		m_Mesh.Optimize();
	}
}
