using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : NetworkBehaviour
{
	public int MaxHealth;
	[HideInInspector]
	public int CurrentHealth;

    public delegate void KilledEventHandler();
    public event KilledEventHandler KilledHandler;

    public override void OnStartServer()
    {
        CurrentHealth = MaxHealth;
    }

    public void Hit(int health)
	{
		CurrentHealth -= health;

        Debug.Log("Health: " + CurrentHealth.ToString());

		if (CurrentHealth <= 0)
        {
            OnKilled();
        }
	}

    protected virtual void OnKilled()
    {
        KilledHandler?.Invoke();
    }
}
