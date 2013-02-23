using UnityEngine;
using System.Collections.Generic;

public class GridData
{
	//Container of Grid cells as Unity GameObjects
	public List<Vector3> CellPos;
	public int NumberOfCells = 0;
	public int NumOfColumns = 0;
	
	public int CellSize = 1;
	public Vector3 Origin;
}
