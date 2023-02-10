using System;
using System.Collections.Generic;
using UnityEngine;

// Created a dedicated animator class to animate the various 
// unit actions like moving and shooting
public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform bulletProjectilePrefab;
    [SerializeField] private Transform shootPointTransform;

    private void Awake()
    {
        if(TryGetComponent<MoveAction>(out MoveAction moveAction))
        {
            moveAction.OnStartMoving += MoveAction_OnStartMoving;
            moveAction.OnStopMoving += MoveAction_OnStopMoving;
        }

        if(TryGetComponent<ShootAction>(out ShootAction shootAction))
        {
            shootAction.OnShoot += ShootAction_OnShoot;
        }
    }

    private void MoveAction_OnStartMoving(object sender, EventArgs e)
    {
        animator.SetBool("isWalking", true);
    }
    private void MoveAction_OnStopMoving(object sender, EventArgs e)
    {
        animator.SetBool("isWalking", false);
    }

    private void ShootAction_OnShoot(object sender, ShootAction.OnShootEventArgs e)
    {
        animator.SetTrigger("Shoot");

        Transform bulletProjectileTransform = 
            Instantiate(bulletProjectilePrefab, shootPointTransform.position, Quaternion.identity);
        BulletProjectile bulletProjectile = bulletProjectileTransform.GetComponent<BulletProjectile>();

        Vector3 targetUnitShootAtPosition = e.targetUnit.GetWorldPosition();
        // Ensures that the bullet travels straight on and not towards the targets feet (the feet is the origin)
        targetUnitShootAtPosition.y = shootPointTransform.position.y;

        bulletProjectile.Setup(targetUnitShootAtPosition);
    }
}
