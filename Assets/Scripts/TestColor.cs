using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[RequireComponent (typeof (MeshCollider))]
[RequireComponent (typeof (MeshFilter))]
[RequireComponent (typeof (MeshRenderer))]
[RequireComponent (typeof (HLayer))]

public class TestColor : HSprite
{

	[HideInInspector]
	[SerializeField]
	private List<Color> m_VertColors = new List<Color>();

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	override public void Rebuild()
	{
		vertList.Clear();
		uvList.Clear();
		triList.Clear();
		m_VertColors.Clear();
		
	
		
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
		
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		if(meshRenderer == null)
		{
			Debug.LogError("MeshRender not found!");
			return;
		}
		meshRenderer.material.shader = Shader.Find("Custom/GridShader");
		
		m_Mesh.Clear();
		
		vertList.Add(new Vector3(-2.0f, -0.1f, 0.0f));
		vertList.Add(new Vector3(-1.0f, -0.1f, 0.0f));
		vertList.Add(new Vector3(-1.0f, 0.1f, 0.0f));
		vertList.Add(new Vector3(-2.0f, 0.1f, 0.0f));
		
		triList.Add(0); triList.Add(3); triList.Add(1);
		triList.Add(3); triList.Add(2); triList.Add(1);
		
		m_VertColors.Add(Color.white);
		m_VertColors.Add(Color.white);
		m_VertColors.Add(Color.white);
		m_VertColors.Add(Color.white);
		
		vertList.Add(new Vector3(-1.0f, -0.1f, 0.0f));
		vertList.Add(new Vector3(0.0f, -0.1f, 0.0f));
		vertList.Add(new Vector3(0.0f, 0.1f, 0.0f));
		vertList.Add(new Vector3(-1.0f, 0.1f, 0.0f));
		
		triList.Add(4); triList.Add(7); triList.Add(5);
		triList.Add(7); triList.Add(6); triList.Add(5);
		
		m_VertColors.Add(Color.green);
		m_VertColors.Add(Color.green);
		m_VertColors.Add(Color.green);
		m_VertColors.Add(Color.green);
		
		vertList.Add(new Vector3(0.0f, -0.1f, 0.0f));
		vertList.Add(new Vector3(1.0f, -0.1f, 0.0f));
		vertList.Add(new Vector3(1.0f, 0.1f, 0.0f));
		vertList.Add(new Vector3(0.0f, 0.1f, 0.0f));
		
		triList.Add(8); triList.Add(11); triList.Add(9);
		triList.Add(11); triList.Add(10); triList.Add(9);
		
		m_VertColors.Add(Color.white);
		m_VertColors.Add(Color.white);
		m_VertColors.Add(Color.white);
		m_VertColors.Add(Color.white);
		
		m_Mesh.vertices = vertList.ToArray();
		m_Mesh.triangles = triList.ToArray();
		m_Mesh.uv1 = uvList.ToArray();
		m_Mesh.colors = m_VertColors.ToArray();
		
	
		m_Mesh.RecalculateNormals();
		m_Mesh.RecalculateBounds();
		m_Mesh.Optimize();
		
	}
}
