using UnityEngine;
using System.Collections;

public class TestViewSwitcher : MonoBehaviour
{
	public int m_VisibleSide = 0;
	private int m_Side = 0;
	public WallsManager m_WManager = null;
	public GridManager m_GManager= null;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(m_VisibleSide == 0)
		{
			if(m_VisibleSide != m_Side)
			{
				m_Side = m_VisibleSide;
				m_WManager.SetActiveSide(ViewState.Default);
				m_GManager.SetActiveSide(ViewState.Default);
			}
		}
		else if(m_VisibleSide == 1)
		{
			if(m_VisibleSide != m_Side)
			{
				m_Side = m_VisibleSide;
				m_WManager.SetActiveSide(ViewState.East);
				m_GManager.SetActiveSide(ViewState.East);
			}
		}
		else if(m_VisibleSide == 2)
		{
			if(m_VisibleSide != m_Side)
			{
				m_Side = m_VisibleSide;
				m_WManager.SetActiveSide(ViewState.West);
				m_GManager.SetActiveSide(ViewState.West);
			}
		}
		else if(m_VisibleSide == 3)
		{
			if(m_VisibleSide != m_Side)
			{
				m_Side = m_VisibleSide;
				m_WManager.SetActiveSide(ViewState.South);
				m_GManager.SetActiveSide(ViewState.South);
			}
		}
	}
}
