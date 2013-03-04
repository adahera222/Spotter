using UnityEngine;
using System.Collections;

public class TestDepthScript : MonoBehaviour
{
	public MapManager m_MapManager = null;
	public GameObject m_SelectObj = null;
	
	private GameObject obj = null;
	private HSprite sSprite = null;
	HLayer sLayer = null;
	
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
		/*
		if(m_SelectTrans != null)
		{
			m_SelectTrans.parent = transform;
		}
		*/
		
	
		obj = Instantiate(m_SelectObj) as GameObject;
		Vector3 selectPos = new Vector3(0.0f, 0.0f, 0.0f);
		obj.transform.position = selectPos;
			
		sSprite = obj.GetComponent<HSprite>();
		if(sSprite == null)
		{
			Debug.LogError("HSprite not found!");
			return;
		}
		
		sLayer = obj.GetComponent<HLayer>();
		if(sLayer == null)
		{
			Debug.LogError("HSprite not found!");
			return;
		}
		
		sLayer.SetLayer(DepthLayer.GridEffects);
		sSprite.Rebuild();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_MapManager != null)
		{
			Vector3 pos = transform.position;
			Vector3 gridPos = m_MapManager.GetNearestSquare(pos.x, pos.y);
			m_Layer.SetDepthOffset(gridPos.z);
			
			Vector3 sPos =  obj.transform.position;
			sPos.x = gridPos.x; sPos.y = gridPos.y;
			obj.transform.position = sPos;
			
			
		}
	}
}
