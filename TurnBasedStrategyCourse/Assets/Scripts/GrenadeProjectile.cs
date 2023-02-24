using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    private Vector3 targetPosition;

    private void Update()
    {
        Vector3 moveDir = (targetPosition - transform.position).normalized;

        float moveSpeed = 15f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        float reachedTargetDistance = 0.2f;
        if(Vector3.Distance(transform.position, targetPosition) < reachedTargetDistance)
        {
            Destroy(gameObject);
        }
    }

    public void Setup(GridPosition targetGridPosition)
    {
        targetPosition = LevelGrid.Instance.GetWorldPosition(targetGridPosition);
    }
}
