using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unit stores a reference to the moveAction (which is responsible for moving the unit)
// and the values of the gridPosition the unit is standing on
public class Unit : MonoBehaviour
{
    private GridPosition gridPosition;
    private MoveAction moveAction;

    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
    }

    private void Start()
    {
        // Unit calculates its own position relative to the floor grid
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        // Unit calles this function which allows the gridObject the unit stands on to
        // store a reference to the unit
        LevelGrid.Instance.AddUnitToGridPosition(gridPosition, this);
    }

    private void Update() 
    {
        // Checks to see if the gridObject below the Unit has changed
        // if gridPosition has changed, call the UnitMovedToGridPosition function from LevelGrid
        // to update the unit reference stored by the GridObject
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if(newGridPosition != gridPosition)
        {
            LevelGrid.Instance.UnitMovedToGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    public MoveAction GetMoveAction()
    {
        return moveAction;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
}
