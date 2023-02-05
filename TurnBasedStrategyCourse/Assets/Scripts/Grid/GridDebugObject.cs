using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridDebugObject : MonoBehaviour
{
    private GridObject gridObject;
    [SerializeField] private TextMeshPro floorText;


    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }

    private void Update() 
    {
        SetDebugVisual();
    }

    private void SetDebugVisual()
    {
        //floorText.text = $"x: {gridObject.gridPosition.x}; z: {gridObject.gridPosition.z}";
        floorText.text = gridObject.ToString();
    }
}
