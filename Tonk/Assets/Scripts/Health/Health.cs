using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
	public int StartingHealth;
	[HideInInspector]
	public int CurrentHealth;

	public Health()
	{
		CurrentHealth = StartingHealth;
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
