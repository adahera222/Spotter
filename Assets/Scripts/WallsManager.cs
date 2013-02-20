using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class WallsManager : MonoBehaviour
{
	public string m_WallsFilePath = "";
	public GameObject m_Walls = null;
	
	private ViewState m_CurrentState = ViewState.Default;
	
	private List<GameObject> m_DefaultList = new List<GameObject>();
	private List<GameObject> m_EastList = new List<GameObject>();
	private List<GameObject> m_WestList = new List<GameObject>();
	private List<GameObject> m_SouthList = new List<GameObject>();
	
	private IsoWall m_WallScript = null;
	
	//==================================
	// Use this for initialization
	//==================================
	void Start () 
	{
		if(m_WallsFilePath != null)
		{
			//Load map Data
			LoadWallsFromFile(m_WallsFilePath);
			//Set angle to default
			m_CurrentState = ViewState.Default;
			
		}
	}
	
	//===================================
	// Update is called once per frame
	//===================================
	void Update ()
	{
	
	}
	
	//=================================
	//Load walls sprites from file
	//==================================
	void LoadWallsFromFile(string filePath)
	{
		if(m_Walls == null)
		{
			Debug.LogError("No game object to render");
			return;
		}
		
		m_WallScript = m_Walls.GetComponent<IsoWall>();
		if(m_WallScript == null)
		{
			Debug.LogError("GameObject does not contain IsoWall script");
			return;
		}
		
		XmlDocument doc = new XmlDocument();
		doc.Load(filePath);
		
		XmlNodeList eRootList = doc.GetElementsByTagName("Root");
		XmlElement eRoot = (XmlElement)eRootList.Item(0);
		string type = eRoot.GetAttribute("Type");
		
			//This is an actual map file
		if(type == "WallsFile")
		{
			//Get Deafult side =======================================
			XmlNodeList rlist = eRoot.GetElementsByTagName("Default");
			XmlElement eDefault = (XmlElement)rlist.Item(0);
			
			XmlNodeList wlist =  eDefault.GetElementsByTagName("Wall");
			int i;
			for(i=0; i<wlist.Count; i++)
			{
				XmlElement eWall = (XmlElement)wlist.Item(i);
				//get position data
				XmlNodeList plist = eWall.GetElementsByTagName("Pos");
				XmlElement ePos= (XmlElement)plist.Item(0);
				//get wall shape data
				XmlNodeList wdlist = eWall.GetElementsByTagName("WD");
				XmlElement eWD = (XmlElement)wdlist.Item(0);
				
				float px = float.Parse(ePos.GetAttribute("X"));
				float py = float.Parse(ePos.GetAttribute("Y"));
				float pz = float.Parse(ePos.GetAttribute("Z"));
				
				float wLength = float.Parse(eWD.GetAttribute("Length"));
				float wHeight = float.Parse(eWD.GetAttribute("Height"));
				float wROffset = float.Parse(eWD.GetAttribute("ROffset"));
				float wLOffset = float.Parse(eWD.GetAttribute("LOffset"));
				
				//Create Grid Cell
				m_DefaultList.Add(Instantiate(m_Walls, new Vector3(px, py, pz), Quaternion.identity) as GameObject);
				IsoWall isoWallScript = m_DefaultList[m_DefaultList.Count - 1].GetComponent<IsoWall>();
				
				isoWallScript.m_LWOffset = wLOffset;
				isoWallScript.m_RWOffset = wROffset;
				isoWallScript.m_WallLength = wLength;
				isoWallScript.m_WallHeight = wHeight;
				isoWallScript.Rebuild();
			}	
			
			//Get East side =======================================
			rlist = eRoot.GetElementsByTagName("East");
			XmlElement eEast = (XmlElement)rlist.Item(0);
			
		    wlist =  eEast.GetElementsByTagName("Wall");
			for(i=0; i<wlist.Count; i++)
			{
				XmlElement eWall = (XmlElement)wlist.Item(i);
				//get position data
				XmlNodeList plist = eWall.GetElementsByTagName("Pos");
				XmlElement ePos= (XmlElement)plist.Item(0);
				//get wall shape data
				XmlNodeList wdlist = eWall.GetElementsByTagName("WD");
				XmlElement eWD = (XmlElement)wdlist.Item(0);
				
				float px = float.Parse(ePos.GetAttribute("X"));
				float py = float.Parse(ePos.GetAttribute("Y"));
				float pz = float.Parse(ePos.GetAttribute("Z"));
				
				float wLength = float.Parse(eWD.GetAttribute("Length"));
				float wHeight = float.Parse(eWD.GetAttribute("Height"));
				float wROffset = float.Parse(eWD.GetAttribute("ROffset"));
				float wLOffset = float.Parse(eWD.GetAttribute("LOffset"));
				
				//Create Grid Cell
				m_EastList.Add(Instantiate(m_Walls, new Vector3(px, py, pz), Quaternion.identity) as GameObject);
				IsoWall isoWallScript = m_EastList[m_EastList.Count - 1].GetComponent<IsoWall>();
				m_EastList[m_EastList.Count - 1].GetComponent<MeshRenderer>().enabled = false;
				
				isoWallScript.m_LWOffset = wLOffset;
				isoWallScript.m_RWOffset = wROffset;
				isoWallScript.m_WallLength = wLength;
				isoWallScript.m_WallHeight = wHeight;
				isoWallScript.Rebuild();
			}	
			
			//Get West side =======================================
			rlist = eRoot.GetElementsByTagName("West");
			XmlElement eWest = (XmlElement)rlist.Item(0);
			
		    wlist =  eWest.GetElementsByTagName("Wall");
			for(i=0; i<wlist.Count; i++)
			{
				XmlElement eWall = (XmlElement)wlist.Item(i);
				//get position data
				XmlNodeList plist = eWall.GetElementsByTagName("Pos");
				XmlElement ePos= (XmlElement)plist.Item(0);
				//get wall shape data
				XmlNodeList wdlist = eWall.GetElementsByTagName("WD");
				XmlElement eWD = (XmlElement)wdlist.Item(0);
				
				float px = float.Parse(ePos.GetAttribute("X"));
				float py = float.Parse(ePos.GetAttribute("Y"));
				float pz = float.Parse(ePos.GetAttribute("Z"));
				
				float wLength = float.Parse(eWD.GetAttribute("Length"));
				float wHeight = float.Parse(eWD.GetAttribute("Height"));
				float wROffset = float.Parse(eWD.GetAttribute("ROffset"));
				float wLOffset = float.Parse(eWD.GetAttribute("LOffset"));
				
				//Create Grid Cell
				m_WestList.Add(Instantiate(m_Walls, new Vector3(px, py, pz), Quaternion.identity) as GameObject);
				IsoWall isoWallScript = m_WestList[m_WestList.Count - 1].GetComponent<IsoWall>();
				m_WestList[m_WestList.Count - 1].GetComponent<MeshRenderer>().enabled = false;
				
				
				isoWallScript.m_LWOffset = wLOffset;
				isoWallScript.m_RWOffset = wROffset;
				isoWallScript.m_WallLength = wLength;
				isoWallScript.m_WallHeight = wHeight;
				isoWallScript.Rebuild();
			}
			
			//Get West side =======================================
			rlist = eRoot.GetElementsByTagName("West");
			XmlElement eSouth = (XmlElement)rlist.Item(0);
			
		    wlist =  eSouth.GetElementsByTagName("Wall");
			for(i=0; i<wlist.Count; i++)
			{
				XmlElement eWall = (XmlElement)wlist.Item(i);
				//get position data
				XmlNodeList plist = eWall.GetElementsByTagName("Pos");
				XmlElement ePos= (XmlElement)plist.Item(0);
				//get wall shape data
				XmlNodeList wdlist = eWall.GetElementsByTagName("WD");
				XmlElement eWD = (XmlElement)wdlist.Item(0);
				
				float px = float.Parse(ePos.GetAttribute("X"));
				float py = float.Parse(ePos.GetAttribute("Y"));
				float pz = float.Parse(ePos.GetAttribute("Z"));
				
				float wLength = float.Parse(eWD.GetAttribute("Length"));
				float wHeight = float.Parse(eWD.GetAttribute("Height"));
				float wROffset = float.Parse(eWD.GetAttribute("ROffset"));
				float wLOffset = float.Parse(eWD.GetAttribute("LOffset"));
				
				//Create Grid Cell
				m_SouthList.Add(Instantiate(m_Walls, new Vector3(px, py, pz), Quaternion.identity) as GameObject);
				IsoWall isoWallScript = m_SouthList[m_SouthList.Count - 1].GetComponent<IsoWall>();
				m_SouthList[m_SouthList.Count - 1].GetComponent<MeshRenderer>().enabled = false;
				
				isoWallScript.m_LWOffset = wLOffset;
				isoWallScript.m_RWOffset = wROffset;
				isoWallScript.m_WallLength = wLength;
				isoWallScript.m_WallHeight = wHeight;
				isoWallScript.Rebuild();
			}
			

		}
	}
	
	//==============================================
	// Set which side is visible
	//==============================================
	public void SetActiveSide(ViewState viewState)
	{
		//Nothings changed
		if(m_CurrentState == viewState)
			return;
		
		m_CurrentState = viewState;
		
		switch (m_CurrentState)
		{
			case ViewState.Default:
			{
				GameObject obj = null;
				int i = 0;
				for(i=0; i<m_DefaultList.Count; i++)
				{
					obj = m_DefaultList[i];
					obj.GetComponent<MeshRenderer>().enabled = true;
				}
				for(i=0; i<m_EastList.Count; i++)
				{
					obj = m_EastList[i];
					obj.GetComponent<MeshRenderer>().enabled = false;
				}
				for(i=0; i<m_WestList.Count; i++)
				{
					obj = m_WestList[i];
					obj.GetComponent<MeshRenderer>().enabled = false;
				}
				for(i=0; i<m_SouthList.Count; i++)
				{
					obj = m_SouthList[i];
					obj.GetComponent<MeshRenderer>().enabled = false;
				}
			
				break;
			}
			case ViewState.East:
			{
				GameObject obj = null;
				int i = 0;
				for(i=0; i<m_DefaultList.Count; i++)
				{
					obj = m_DefaultList[i];
					obj.GetComponent<MeshRenderer>().enabled = false;
				}
				for(i=0; i<m_EastList.Count; i++)
				{
					obj = m_EastList[i];
					obj.GetComponent<MeshRenderer>().enabled = true;
				}
				for(i=0; i<m_WestList.Count; i++)
				{
					obj = m_WestList[i];
					obj.GetComponent<MeshRenderer>().enabled = false;
				}
				for(i=0; i<m_SouthList.Count; i++)
				{
					obj = m_SouthList[i];
					obj.GetComponent<MeshRenderer>().enabled = false;
				}
			
				break;
			}
			case ViewState.West:
			{
				GameObject obj = null;
				int i = 0;
				for(i=0; i<m_DefaultList.Count; i++)
				{
					obj = m_DefaultList[i];
					obj.GetComponent<MeshRenderer>().enabled = false;
				}
				for(i=0; i<m_EastList.Count; i++)
				{
					obj = m_EastList[i];
					obj.GetComponent<MeshRenderer>().enabled = false;
				}
				for(i=0; i<m_WestList.Count; i++)
				{
					obj = m_WestList[i];
					obj.GetComponent<MeshRenderer>().enabled = true;
				}
				for(i=0; i<m_SouthList.Count; i++)
				{
					obj = m_SouthList[i];
					obj.GetComponent<MeshRenderer>().enabled = false;
				}
			
				break;
			}
			case ViewState.South:
			{
				GameObject obj = null;
				int i = 0;
				for(i=0; i<m_DefaultList.Count; i++)
				{
					obj = m_DefaultList[i];
					obj.GetComponent<MeshRenderer>().enabled = false;
				}
				for(i=0; i<m_EastList.Count; i++)
				{
					obj = m_EastList[i];
					obj.GetComponent<MeshRenderer>().enabled = false;
				}
				for(i=0; i<m_WestList.Count; i++)
				{
					obj = m_WestList[i];
					obj.GetComponent<MeshRenderer>().enabled = false;
				}
				for(i=0; i<m_SouthList.Count; i++)
				{
					obj = m_SouthList[i];
					obj.GetComponent<MeshRenderer>().enabled = true;
				}
			
				break;
			}	
		}
	}
}
