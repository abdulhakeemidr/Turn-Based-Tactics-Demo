using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MoveAction is responsible for moving the unit character around
// the grids with a preset max move distance between grids
// MoveAction also checks which moves are valid for the unit
public class MoveAction : MonoBehaviour
{
    [SerializeField]
    private Animator unitAnimator;
    [SerializeField]
    private int maxMoveDistance = 4;

    private Vector3 targetPosition;
    private Unit unit;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        targetPosition = transform.position;
    }

    void Update()
    {
        float stopDistance = 0.1f;
        if(Vector3.Distance(transform.position, targetPosition) > stopDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

            unitAnimator.SetBool("isWalking",true);
        }
        else
        {
            unitAnimator.SetBool("isWalking",false);
        }
    }

    // Sets the targetPosition to the centre of the grid in Vector3 terms
    public void Move(GridPosition targetGridPositon)
    {
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(targetGridPositon);
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }

    // This function gets all valid move positions for a unit
    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for(int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for(int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                // Removes gridPositions outside the bounds of the grid array
                if(!LevelGrid.Instance.isValidGridPosition(testGridPosition))
                {
                    continue;
                }

                // Same Grid Position where the unit is already at
                if(unitGridPosition == testGridPosition)
                {
                    continue;
                }

                // Grid Position already occupied with another unit
                if(LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }
                //Debug.Log(testGridPosition);
                validGridPositionList.Add(testGridPosition);
            }
        }
        return validGridPositionList;
    }
}