using System;
using System.Collections.Generic;
using UnityEngine;

// Controls the health of the Unit and sends out events
// for what happens when the health is over
public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDead;
    [SerializeField] private int health = 100;

    public void Damage(int damageAmount)
    {
        health -= damageAmount;

        if(health < 0) health = 0;

        if(health == 0)
        {
            Die();
        }

        Debug.Log(health);
    }

    private void Die()
    {
        OnDead?.Invoke(this, EventArgs.Empty);
    }
}
