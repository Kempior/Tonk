using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : NetworkBehaviour
{
	public int MaxHealth;
	[HideInInspector]
	public int CurrentHealth;

	public Health()
	{
		CurrentHealth = MaxHealth;
	}

	public void Hit(int health)
	{
		CurrentHealth -= health;

		if (CurrentHealth < 0)
			Die();
		else
			HitEffects(health);
	}

	public abstract void HitEffects(int health);

	public abstract void Die();
}
