using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit unit;

    private void Start()
    {

    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            GridSystemVisual.Instance.HideAllGridPositionVisual();
            //GridSystemVisual.Instance.ShowGridPositionList(
            //    unit.GetMoveAction().GetValidActionGridPositionList());
        }
    }
}
