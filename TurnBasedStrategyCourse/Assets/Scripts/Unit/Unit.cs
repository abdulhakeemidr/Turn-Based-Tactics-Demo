using System;
using System.Collections.Generic;
using UnityEngine;

// Unit stores a reference to the moveAction (which is responsible for moving the unit)
// and the values of the gridPosition the unit is standing on
public class Unit : MonoBehaviour
{
    private const int ACTION_POINTS_MAX = 9;
    public static event EventHandler onAnyActionPointsChanged;
    public static event EventHandler onAnyUnitSpawned;
    public static event EventHandler onAnyUnitDead;

    [SerializeField] private bool isEnemy;
    private GridPosition gridPosition;
    private HealthSystem healthSystem;
    private BaseAction[] baseActionArray;
    private int actionPoints = ACTION_POINTS_MAX;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        baseActionArray = GetComponents<BaseAction>();
    }

    private void Start()
    {
        // Unit calculates its own position relative to the floor grid
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        // Unit calles this function which allows the gridObject the unit stands on to
        // store a reference to the unit
        LevelGrid.Instance.AddUnitToGridPosition(gridPosition, this);
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

        healthSystem.OnDead += HealthSystem_OnDead;

        onAnyUnitSpawned?.Invoke(this, EventArgs.Empty);
    }

    private void Update() 
    {
        // Checks to see if the gridObject below the Unit has changed
        // if gridPosition has changed, call the UnitMovedToGridPosition function from LevelGrid
        // to update the unit reference stored by the GridObject
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if(newGridPosition != gridPosition)
        {
            GridPosition oldGridPosition = gridPosition;
            gridPosition = newGridPosition;
            LevelGrid.Instance.UnitMovedToGridPosition(this, oldGridPosition, newGridPosition);
        }
    }

    public T GetAction<T>() where T : BaseAction
    {
        foreach(BaseAction baseAction in baseActionArray)
        {
            if(baseAction is T)
            {
                return (T)baseAction;
            }
        }
        return null;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    public BaseAction[] GetBaseActionArray()
    {
        return baseActionArray;
    }

    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if(CanSpendActionPointsToTakeAction(baseAction))
        {
            SpendActionPoints(baseAction.GetActionPointsCost());
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanSpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if(actionPoints >= baseAction.GetActionPointsCost()) return true;
        else return false;
    }

    private void SpendActionPoints(int amount)
    {
        actionPoints -= amount;

        onAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetActionPoints()
    {
        return actionPoints;
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        // if the unit is an enemy and it is not the players turn OR
        // if the unit is the player (not an enemy) and it is the players turn
        if((IsEnemy() && !TurnSystem.Instance.IsPlayerTurn()) ||
            (!IsEnemy() && TurnSystem.Instance.IsPlayerTurn()))
        {
            actionPoints = ACTION_POINTS_MAX;

            onAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
        }

    }

    public bool IsEnemy()
    {
        return isEnemy;
    }

    public void Damage(int damageAmount)
    {
        healthSystem.Damage(damageAmount);
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        LevelGrid.Instance.RemoveUnitAtGridPosition(gridPosition, this);
        Destroy(gameObject);

        onAnyUnitDead?.Invoke(this, EventArgs.Empty);
    }

    public float GetHealthNormalized()
    {
        return healthSystem.GetHealthNormalized();
    }
}
