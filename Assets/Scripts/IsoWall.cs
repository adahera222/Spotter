using UnityEngine;
using System.Collections;

//[RequireComponent (typeof (MeshCollider))]
[RequireComponent (typeof (MeshFilter))]
[RequireComponent (typeof (MeshRenderer))]

public class IsoWall : MonoBehaviour
{
	public float m_WallLength = 5.0f;
	public float m_WallHeight = 5.0f;
	
	public float m_LWOffset = 0.0f;
	public float m_RWOffset = 0.0f;
	
	private Mesh m_Mesh = null;
	private GameObject m_Temp;
	//==========================================
	// Use this for initialization
	//=========================================
	void Start ()
	{
		
		
		
	}
	
	//==============================================
	// Update is called once per frame
	//=============================================
	void Update ()
	{

	}
	
	//=============================================
	// Rebuild the mesh
	//==============================================
	public void Rebuild()
	{
		
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		if(meshFilter == null)
		{
			Debug.LogError("MeshFilter not found!");
			return;
		}
		
		Vector3 p0 = new Vector3(0.0f * m_WallLength, 0.0f * m_WallHeight + m_LWOffset, 0.0f);
		Vector3 p1 = new Vector3(1.0f * m_WallLength, 0.0f * m_WallHeight + m_RWOffset,0.0f);
		Vector3 p2 = new Vector3(1.0f * m_WallLength, 1.0f * m_WallHeight + m_RWOffset, 0.0f);
		Vector3 p3 = new Vector3(0.0f * m_WallLength, 1.0f * m_WallHeight + m_LWOffset, 0.0f);
		
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
	
	public void SetGameObj(GameObject obj)
	{
		m_Temp = obj;
	}
}
