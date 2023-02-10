using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enables and disables the selected units visual indicator
// when it is selected
// When selected, the UnitActionSystem fires an event to notify that
// a unit has been selected
public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField]
    private Unit unit;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        UpdateVisual();
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs empty)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        // enables the meshrenderer if the selected unit is this unit
        if(UnitActionSystem.Instance.GetSelectedUnit() == unit)
        {
            meshRenderer.enabled = true;
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }

    private void OnDestroy()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged -= UnitActionSystem_OnSelectedUnitChanged;
    }
}
