using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Grenade Action throws a grenade
public class GrenadeAction : BaseAction
{
    [SerializeField] private GameObject grenadeProjectilePrefab;
    private int maxThrowDistance = 7;
    private void Update()
    {
        if(!isActive)
        {
            return;
        }

        ActionComplete();
    }
    public override string GetActionName()
    {
        return "Grenade";
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = 0
        };
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        for(int x = -maxThrowDistance; x <= maxThrowDistance; x++)
        {
            for(int z = -maxThrowDistance; z <= maxThrowDistance; z++)
            {
                // Gets the test grid position that is x and z grid positions away from the unit's grid position
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                // Removes gridPositions outside the bounds of the grid array
                if(!LevelGrid.Instance.isValidGridPosition(testGridPosition))
                {
                    continue;
                }
                // Skips gridPositions outside the shooting range
                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if(testDistance > maxThrowDistance)
                {
                    continue;
                }
                 
                //Debug.Log(testGridPosition);
                validGridPositionList.Add(testGridPosition);
            }
        }
        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        GameObject grenadeProjectileTransform = Instantiate(grenadeProjectilePrefab, unit.GetWorldPosition(), Quaternion.identity);
        GrenadeProjectile grenadeProjectile = grenadeProjectileTransform.GetComponent<GrenadeProjectile>();
        grenadeProjectile.Setup(gridPosition);
        
        ActionStart(onActionComplete);
    }
}
