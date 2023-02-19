using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GridObject is an instance of a grid
// GridObject stores the value of the gridPosition of each grid in a GridObject instance
// It also stores a reference to any units that are standing on a grid
public class GridObject
{
    private GridSystem<GridObject> gridSystem;
    private GridPosition gridPosition;
    private List<Unit> unitListOnGrid;

    public GridObject(GridSystem<GridObject> gridSystem, GridPosition gridPosition)
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

    public bool HasAnyUnit()
    {
        return unitListOnGrid.Count > 0;
    }

    public Unit GetUnit()
    {
        if(HasAnyUnit())
        {
            return unitListOnGrid[0];
        }
        else
        {
            return null;
        }
    }
}
