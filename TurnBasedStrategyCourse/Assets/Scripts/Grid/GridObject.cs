using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;
    private List<Unit> unitListOnGrid;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        unitListOnGrid = new List<Unit>();
    }

    public List<Unit> GetUnitList()
    {
        return unitListOnGrid;
    }

    public void AddUnit(Unit unit)
    {
        unitListOnGrid.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        unitListOnGrid.Remove(unit);
    }

    public override string ToString()
    {
        string unitString = "";
        foreach(Unit unit in unitListOnGrid)
        {
            unitString += unit + "\n";
        }

        return gridPosition.ToString() + "\n" + unitString;
    }
}
