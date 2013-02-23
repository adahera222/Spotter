using UnityEngine;
using System.Collections.Generic;
using System.Xml;


[ExecuteInEditMode]
public class GridBuilder : MonoBehaviour
{
	public int m_CellSize = 1;
	public Vector3 m_Origin = new Vector3(0.0f, 0.0f, 1.0f);
	public GameObject m_CellObject;
	
	public string m_SavefilePath = "";
	public int m_NumOfCells = 0;
	
	public bool m_NoRot = true;
	public bool m_Default = false;
	public bool m_West = false;
	public bool m_East = false;
	public bool m_South = false;
	public float m_CurrentAngle = 0.0f;
	
	private bool m_FirstColumn = true;
	private int m_NumOfColumns = 0;
	private Vector3 m_FirstCellPos = new Vector3();
	
	//List of Game Objects edit mode only
	public List<GameObject> m_GridList = new List<GameObject>();
	
	//============================================
	// Use this for initialization
	//==============================================
	void Start ()
	{
		/*
		for(int i=0; i<4; i++)
		{
			AddGridColumn(6+i);
		}
		
		for(int i=0; i<m_GridList.Count; i++)
		{
			GameObject gObject = m_GridList[i] as GameObject;
			gObject.transform.RotateAround(m_Origin, new Vector3(0.0f, 0.0f, 1.0f), 45.0f);
		}
		
		SaveGridToFile("Assets\\Maps\\TestMap.xml");
		*/
	}
	
	//=============================================
	// Update is called once per frame
	//==================================================
	void Update ()
	{
	
	}
	
	//==================================================
	//	Add column to grid
	//===================================================
	public void AddGridColumn(int numCells)
	{
		float halfCell = ((float)m_CellSize)/2.0f;
		float columnLength = numCells * m_CellSize;
		
		if(m_FirstColumn)
		{
			//Make sure the first column is set to the middle of the origin
			m_FirstCellPos.y = m_Origin.y - ((columnLength /2.0f) + halfCell);
			for(int i=0; i<numCells; i++)
			{
				if(m_CellObject != null)
				{
					float x = m_Origin.x;
					float y = m_FirstCellPos.y + (i * m_CellSize);
	
					m_GridList.Add(Instantiate(m_CellObject, new Vector3(x, y, m_Origin.z), Quaternion.identity)
						as GameObject);
					m_FirstColumn = false;
					
				}
			}
			m_NumOfColumns++;
			
			
		}
		else
		{
			//Last cell added to the grid will belong to previous column
			GameObject lastCell = null;
			if(m_GridList.Count > 0)
				lastCell= m_GridList[m_GridList.Count - 1] as GameObject;
			/////////////////////////////////////////////////////////////////////	
			if(lastCell != null)
			{
				//Shift all the other coulms over for the new column
				for(int j=0; j<m_GridList.Count; j++)
				{
					GameObject temp = m_GridList[j] as GameObject;
					temp.transform.Translate( -halfCell, 0.0f, 0.0f);
				}
			}
			////////////////////////////////////////////////////////////////////////////
			for(int i=0; i<numCells; i++)
			{
				if(m_CellObject != null)
				{
					float x = lastCell.transform.position.x + m_CellSize;
					float y = m_FirstCellPos.y + (i * m_CellSize);
					
					m_GridList.Add(Instantiate(m_CellObject, new Vector3(x, y, m_Origin.z), Quaternion.identity) as GameObject);
				}
				
			}
			m_NumOfColumns++;
		}
		
		ScaleGrid(m_CellSize);
		
	}
	
	//==============================================================
	// Scale entire grid
	//=================================================================
	public void ScaleGrid(float scale)
	{
		Vector3 vScale = new Vector3(scale, scale, scale);
		for (int i=0; i<m_GridList.Count; i++)
		{
			GameObject temp = m_GridList[i];
			temp.transform.localScale = vScale;
		}
	}
	
	//========================================================
	// Rotate entire grid
	//========================================================
	public void RotateGrid(float angle, Vector3 origin)
	{
		for (int i=0; i<m_GridList.Count; i++)
		{
			GameObject temp = m_GridList[i];
			temp.transform.RotateAround(origin, new Vector3(0.0f, 0.0f, 1.0f), angle);
		}
	}
	
	//======================================================
	// Clear map and remove prefabs
	//=======================================================
	public void ClearMap()
	{
		for (int i=0; i<m_GridList.Count; i++)
		{
			DestroyImmediate(m_GridList[i]);		
		}
		m_GridList.Clear();
		m_FirstColumn = true;
	}
	
	//=================================================
	//	Save to xml file
	//=================================================
	public void SaveGridToFile(string filePath)
	{
		
		XmlDocument doc = new XmlDocument();
		XmlElement eRoot = (XmlElement)doc.AppendChild(doc.CreateElement("Root"));
		eRoot.SetAttribute("Type", "MapFile");
		//Set grid map origin
		XmlElement eOrigin = (XmlElement)eRoot.AppendChild(doc.CreateElement("Origin"));
		eOrigin.SetAttribute("X", m_Origin.x.ToString());
		eOrigin.SetAttribute("Y", m_Origin.y.ToString());
		eOrigin.SetAttribute("Z", m_Origin.z.ToString());
		//Set grid cell size
		XmlElement eSize = (XmlElement)eRoot.AppendChild(doc.CreateElement("Size"));
		eSize.SetAttribute("Size", m_CellSize.ToString());
		//Set all the grid positions
		for (int i=0; i<m_GridList.Count; i++)
		{
		XmlElement ePos = (XmlElement)eRoot.AppendChild(doc.CreateElement("Cell"));
		ePos.SetAttribute("X", m_GridList[i].transform.position.x.ToString());
		ePos.SetAttribute("Y", m_GridList[i].transform.position.y.ToString());
		ePos.SetAttribute("Z", m_GridList[i].transform.position.z.ToString());
		}
		
		doc.Save(filePath);
		
	}
	
}
