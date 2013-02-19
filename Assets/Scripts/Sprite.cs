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
	public float m_Layer = 1.0f;
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
		
	}
}
