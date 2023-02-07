using System;
using System.Collections.Generic;
using UnityEngine;

// UnitActionSystem is a Singleton
// Handles selection of unit and movement of selected unit
public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }
    public event EventHandler OnSelectedUnitChanged;

    [SerializeField]
    private Unit selectedUnit;

    [SerializeField]
    private LayerMask unitLayerMask;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There's more than one UnitActionSystem! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(TryHandleUnitSelection()) return;
            // converts mouse click position into a mouse position on the grid
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            // checks if mouse position on grid is a valid move position for the selected unit
            if(selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
            {
                selectedUnit.GetMoveAction().Move(mouseGridPosition);
            }
            //selectedUnit.GetMoveAction().Move(MouseWorld.GetPosition());
        }
    }

    private bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
        {
            if(raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                //selectedUnit = unit;
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;

        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        // The line above is a compact version of the if statement below
        // if(OnSelectedUnitChanged != null)
        // {
        //     OnSelectedUnitChanged(this, EventArgs.Empty);
        // }
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
