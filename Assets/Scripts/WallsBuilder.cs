using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

[ExecuteInEditMode]
public class WallsBuilder : MonoBehaviour 
{
	
	public string m_SaveFilePath = "";
	public GameObject m_WallObject = null;
	public ViewState m_CurrentState = ViewState.Default;
	public ViewState m_ChangeState = ViewState.Default;
	
	private List<GameObject> m_DefaultList = new List<GameObject>();
	private List<GameObject> m_EastList = new List<GameObject>();
	private List<GameObject> m_WestList = new List<GameObject>();
	private List<GameObject> m_SouthList = new List<GameObject>();
	
	//=======================================
	// Use this for initialization
	//=======================================
	void Start () 
	{
	
	}
	
	//===========================================
	// Update is called once per frame
	//===========================================
	void Update () 
	{
		
	}
	
	//=================================================
	// Create an IsoWall and store in proper container
	//==================================================
	public void CreateWall()
	{
		
		if(m_WallObject == null)
		{
			Debug.LogError("IsoWall Object not found!");
			return;
		}
		
		if(m_CurrentState == ViewState.Default)
			m_DefaultList.Add(Instantiate(m_WallObject, new Vector3(0.0f, 0.0f, 1.0f), Quaternion.identity)as GameObject);
		else if(m_CurrentState == ViewState.East)
			m_EastList.Add(Instantiate(m_WallObject, new Vector3(0.0f, 0.0f, 1.0f), Quaternion.identity)as GameObject);
		else if(m_CurrentState == ViewState.West)
			m_WestList.Add(Instantiate(m_WallObject, new Vector3(0.0f, 0.0f, 1.0f), Quaternion.identity)as GameObject);
		else if(m_CurrentState == ViewState.South)
			m_SouthList.Add(Instantiate(m_WallObject, new Vector3(0.0f, 0.0f, 1.0f), Quaternion.identity)as GameObject);
	}
	
	//===============================================
	// Change the state being worked in
	//==============================================
	public void ChangeState()
	{
		if(m_CurrentState == ViewState.Default)
		{
			//===============================
		for(int i=0; i<m_DefaultList.Count; i++)
			{
				m_DefaultList[i].GetComponent<MeshRenderer>().enabled = true;
			}
			for(int i=0; i<m_EastList.Count; i++)
			{
				m_EastList[i].GetComponent<MeshRenderer>().enabled = false;
			}
			for(int i=0; i<m_WestList.Count; i++)
			{
				m_WestList[i].GetComponent<MeshRenderer>().enabled = false;
			}
			for(int i=0; i<m_SouthList.Count; i++)
			{
				m_SouthList[i].GetComponent<MeshRenderer>().enabled = false;
			}
			
		}
		else if(m_CurrentState == ViewState.East)
		{
			//===========================
			for(int i=0; i<m_DefaultList.Count; i++)
			{
				m_DefaultList[i].GetComponent<MeshRenderer>().enabled = false;
			}
			for(int i=0; i<m_EastList.Count; i++)
			{
				m_EastList[i].GetComponent<MeshRenderer>().enabled = true;
			}
			for(int i=0; i<m_WestList.Count; i++)
			{
				m_WestList[i].GetComponent<MeshRenderer>().enabled = false;
			}
			for(int i=0; i<m_SouthList.Count; i++)
			{
				m_SouthList[i].GetComponent<MeshRenderer>().enabled = false;
			}
		}
		else if(m_CurrentState == ViewState.West)
		{
			//============================
		for(int i=0; i<m_DefaultList.Count; i++)
			{
				m_DefaultList[i].GetComponent<MeshRenderer>().enabled = false;
			}
			for(int i=0; i<m_EastList.Count; i++)
			{
				m_EastList[i].GetComponent<MeshRenderer>().enabled = false;
			}
			for(int i=0; i<m_WestList.Count; i++)
			{
				m_WestList[i].GetComponent<MeshRenderer>().enabled = true;
			}
			for(int i=0; i<m_SouthList.Count; i++)
			{
				m_SouthList[i].GetComponent<MeshRenderer>().enabled = false;
			}
		}
		else if(m_CurrentState == ViewState.South)
		{
			//============================
			for(int i=0; i<m_DefaultList.Count; i++)
			{
				m_DefaultList[i].GetComponent<MeshRenderer>().enabled = false;
			}
			for(int i=0; i<m_EastList.Count; i++)
			{
				m_EastList[i].GetComponent<MeshRenderer>().enabled = false;
			}
			for(int i=0; i<m_WestList.Count; i++)
			{
				m_WestList[i].GetComponent<MeshRenderer>().enabled = false;
			}
			for(int i=0; i<m_SouthList.Count; i++)
			{
				m_SouthList[i].GetComponent<MeshRenderer>().enabled = true;
			}
		}	
	}
	
	//================================================
	// Clear Current side's list
	//=================================================
	 public void ClearSide()
	{
		if(m_CurrentState == ViewState.Default)
		{
			for(int i=0; i<m_DefaultList.Count; i++)
			{
				DestroyImmediate(m_DefaultList[i]);			
			}	
			m_DefaultList.Clear();
		}
		else if(m_CurrentState == ViewState.East)
		{
			for(int i=0; i<m_EastList.Count; i++)
			{
				DestroyImmediate(m_EastList[i]);
			}	
			m_EastList.Clear();
		}
		else if(m_CurrentState == ViewState.West)
		{
			for(int i=0; i<m_WestList.Count; i++)
			{
				DestroyImmediate(m_WestList[i]);		
			}
			m_WestList.Clear();
		}
		else if(m_CurrentState == ViewState.South)
		{
			for(int i=0; i<m_SouthList.Count; i++)
			{
				DestroyImmediate(m_SouthList[i]);		
			}
			m_SouthList.Clear();
		}	
	}
	
	//===========================================================
	// Clear All lists
	//=========================================================
	public void ClearAll()
	{
	
		for(int i=0; i<m_DefaultList.Count; i++)
		{
			DestroyImmediate(m_DefaultList[i]);	
		}	
		for(int i=0; i<m_EastList.Count; i++)
		{
			DestroyImmediate(m_EastList[i]);	
		}	
		for(int i=0; i<m_WestList.Count; i++)
		{
			DestroyImmediate(m_WestList[i]);		
		}
		for(int i=0; i<m_SouthList.Count; i++)
		{
			DestroyImmediate(m_SouthList[i]);		
		}
		
		m_DefaultList.Clear();	
		m_EastList.Clear();
		m_WestList.Clear();
		m_SouthList.Clear();
	}
	
	//===========================================================
	// Save data to xml file
	//============================================================
	public void SaveToXml(string filePath)
	{
		IsoWall isoWall = null;
		
		XmlDocument doc = new XmlDocument();
		XmlElement eRoot = (XmlElement)doc.AppendChild(doc.CreateElement("Root"));
		eRoot.SetAttribute("Type", "WallsFile");
		//Set walls for default side
		XmlElement eSide0 = (XmlElement)eRoot.AppendChild(doc.CreateElement("Default"));
		
		//Set all the wall positions
		for (int i=0; i<m_DefaultList.Count; i++)
		{
			isoWall =  m_DefaultList[i].GetComponent<IsoWall>();
			if(isoWall != null)
			{
				XmlElement eWall = (XmlElement)eSide0.AppendChild(doc.CreateElement("Wall"));
				//Add position
				XmlElement ePos =  (XmlElement)eWall.AppendChild(doc.CreateElement("Pos"));
				ePos.SetAttribute("X", m_DefaultList[i].transform.position.x.ToString());
				ePos.SetAttribute("Y", m_DefaultList[i].transform.position.y.ToString());
				ePos.SetAttribute("Z", m_DefaultList[i].transform.position.z.ToString());
				//Add Wall data
				XmlElement eWD =  (XmlElement)eWall.AppendChild(doc.CreateElement("WD"));
				eWD.SetAttribute("Length", isoWall.m_WallLength.ToString());
				eWD.SetAttribute("Height", isoWall.m_WallHeight.ToString());
				eWD.SetAttribute("LOffset", isoWall.m_LWOffset.ToString());
				eWD.SetAttribute("ROffset", isoWall.m_RWOffset.ToString());
				
			}
		}
		
		//Set walls for east side
		XmlElement eSide1 = (XmlElement)eRoot.AppendChild(doc.CreateElement("East"));
		
		//Set all the wall positions
		for (int i=0; i<m_EastList.Count; i++)
		{
			isoWall =  m_EastList[i].GetComponent<IsoWall>();
			if(isoWall != null)
			{
				XmlElement eWall = (XmlElement)eSide1.AppendChild(doc.CreateElement("Wall"));
				//Add position
				XmlElement ePos =  (XmlElement)eWall.AppendChild(doc.CreateElement("Pos"));
				ePos.SetAttribute("X", m_EastList[i].transform.position.x.ToString());
				ePos.SetAttribute("Y", m_EastList[i].transform.position.y.ToString());
				ePos.SetAttribute("Z", m_EastList[i].transform.position.z.ToString());
				//Add Wall data
				XmlElement eWD =  (XmlElement)eWall.AppendChild(doc.CreateElement("WD"));
				eWD.SetAttribute("Length", isoWall.m_WallLength.ToString());
				eWD.SetAttribute("Height", isoWall.m_WallHeight.ToString());
				eWD.SetAttribute("LOffset", isoWall.m_LWOffset.ToString());
				eWD.SetAttribute("ROffset", isoWall.m_RWOffset.ToString());
				
			}
		}
		
		//Set walls for west side
		XmlElement eSide2 = (XmlElement)eRoot.AppendChild(doc.CreateElement("West"));
		
		//Set all the wall positions
		for (int i=0; i<m_WestList.Count; i++)
		{
			isoWall =  m_WestList[i].GetComponent<IsoWall>();
			if(isoWall != null)
			{
				XmlElement eWall = (XmlElement)eSide2.AppendChild(doc.CreateElement("Wall"));
				//Add position
				XmlElement ePos =  (XmlElement)eWall.AppendChild(doc.CreateElement("Pos"));
				ePos.SetAttribute("X", m_WestList[i].transform.position.x.ToString());
				ePos.SetAttribute("Y", m_WestList[i].transform.position.y.ToString());
				ePos.SetAttribute("Z", m_WestList[i].transform.position.z.ToString());
				//Add Wall data
				XmlElement eWD =  (XmlElement)eWall.AppendChild(doc.CreateElement("WD"));
				eWD.SetAttribute("Length", isoWall.m_WallLength.ToString());
				eWD.SetAttribute("Height", isoWall.m_WallHeight.ToString());
				eWD.SetAttribute("LOffset", isoWall.m_LWOffset.ToString());
				eWD.SetAttribute("ROffset", isoWall.m_RWOffset.ToString());
				
			}
		}
		
		//Set walls for south side
		XmlElement eSide3 = (XmlElement)eRoot.AppendChild(doc.CreateElement("South"));
		
		//Set all the grid positions
		for (int i=0; i<m_SouthList.Count; i++)
		{
			isoWall =  m_SouthList[i].GetComponent<IsoWall>();
			if(isoWall != null)
			{
				XmlElement eWall = (XmlElement)eSide3.AppendChild(doc.CreateElement("Wall"));
				//Add position
				XmlElement ePos =  (XmlElement)eWall.AppendChild(doc.CreateElement("Pos"));
				ePos.SetAttribute("X", m_SouthList[i].transform.position.x.ToString());
				ePos.SetAttribute("Y", m_SouthList[i].transform.position.y.ToString());
				ePos.SetAttribute("Z", m_SouthList[i].transform.position.z.ToString());
				//Add Wall data
				XmlElement eWD =  (XmlElement)eWall.AppendChild(doc.CreateElement("WD"));
				eWD.SetAttribute("Length", isoWall.m_WallLength.ToString());
				eWD.SetAttribute("Height", isoWall.m_WallHeight.ToString());
				eWD.SetAttribute("LOffset", isoWall.m_LWOffset.ToString());
				eWD.SetAttribute("ROffset", isoWall.m_RWOffset.ToString());
				
			}
		}
		
		
		doc.Save(filePath);
		
	}
	
}
