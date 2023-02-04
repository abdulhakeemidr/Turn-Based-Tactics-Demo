using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles the position of the mouse on the movement floor
public class MouseWorld : MonoBehaviour
{
    private static MouseWorld instance;

    [SerializeField]
    private LayerMask mousePlaneLayerMask;

    private void Awake()
    {
        instance = this;
    }

    // private void Update() 
    // {
    //     transform.position = MouseWorld.GetPosition();
    // }

    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneLayerMask);
        return raycastHit.point;
    }
}
