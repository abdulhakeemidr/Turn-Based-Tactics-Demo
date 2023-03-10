using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls the bullet projectile gameObject
// as well as its effects like trails and particle effects
public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Transform bulletHitVfxPrefab;
    private Vector3 targetPosition;
    public void Setup(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    private void Update()
    {
        Vector3 moveDir = (targetPosition - transform.position).normalized;
    
        float distanceBeforeMoving = Vector3.Distance(transform.position, targetPosition);
    
        float moveSpeed = 200f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    
        float distanceAfterMoving = Vector3.Distance(transform.position, targetPosition);
    
        if(distanceBeforeMoving < distanceAfterMoving)
        {
            // ensures the bullet trail doesn't overshoot its target
            transform.position = targetPosition;
            trailRenderer.transform.parent = null;
            Destroy(gameObject);
    
            Instantiate(bulletHitVfxPrefab, targetPosition, Quaternion.identity);
        }
    }
}
