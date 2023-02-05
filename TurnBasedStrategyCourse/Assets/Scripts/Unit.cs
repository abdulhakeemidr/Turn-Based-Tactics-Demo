using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles the unit movement rotation and translation
public class Unit : MonoBehaviour
{
    [SerializeField]
    private Animator unitAnimator;

    private Vector3 targetPosition;
    private GridPosition gridPosition;

    private void Awake()
    {
        targetPosition = transform.position;
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

        // Checks to see if the gridObject below the Unit has changed
        // if gridPosition has changed, 
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if(newGridPosition != gridPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }
    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
