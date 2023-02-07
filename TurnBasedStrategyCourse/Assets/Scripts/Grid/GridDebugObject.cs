using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// GridDebugObject is attached to the 3d Text GridDebugObject prefab
// this script controls the text that is written on the 3d text gameobject
// it displays the position of the gridObject and the name of any unit standing above it
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
