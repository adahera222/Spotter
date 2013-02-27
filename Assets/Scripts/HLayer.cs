using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum DepthLayer
{
	Background = 5,
	Map = 4,
	Actor = 3,
	Effects = 1,
	Default = 0,
}
public class HLayer : MonoBehaviour
{
	public DepthLayer m_Layer = DepthLayer.Default;
	[HideInInspector]
	public float m_ZLayer = 0.0f;
	[HideInInspector]
	public float m_ZOffset = 0.0f;
	//=====================================
	// Use this for initialization
	//=====================================
	void Start () 
	{
	
	}
	
	//======================================
	// Update is called once per frame
	//======================================
	void Update ()
	{
	
	}
	
	//====================================
	// Set Depth layer of object
	//====================================
	public void SetLayer(DepthLayer layer)
	{
		m_Layer = layer;
		m_ZLayer = (int)layer + m_ZOffset;
		//set the transforms depth
		Vector3 pos = transform.position;
		pos.z = m_ZLayer;
		transform.position = pos;
	}
	
	//========================================
	// Set Offset for layer
	//=======================================
	public void SetDepthOffset(float offset)
	{
		m_ZOffset = offset;
		m_ZLayer = (int)m_Layer + m_ZOffset;
		//set the transforms depth
		Vector3 pos = transform.position;
		pos.z = m_ZLayer;
		transform.position = pos;
	}
}
