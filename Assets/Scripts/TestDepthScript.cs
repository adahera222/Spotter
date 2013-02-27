using UnityEngine;
using System.Collections;

public class TestDepthScript : MonoBehaviour
{
	public MapManager m_MapManager = null;
	
	HLayer m_Layer = null;
	// Use this for initialization
	void Start () 
	{
		m_Layer = GetComponent<HLayer>();
		if(m_Layer == null)
		{
			Debug.LogError("HLayer not found!");
			return;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_MapManager != null)
		{
			Vector3 pos = transform.position;
			float z = m_MapManager.GetNearestSquare(pos.x, pos.y).z;
			m_Layer.SetDepthOffset(z);
		}
	}
}
