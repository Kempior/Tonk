using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
	public int StartingHealth;
	[HideInInspector]
	public int CurrentHealth;

	private bool iFrame = false;

	public Health()
	{
		CurrentHealth = StartingHealth;
	}

	public void Hit(int health)
	{
		if (!iFrame)
		{
			CurrentHealth -= health;

			if (CurrentHealth < 0)
				Die();
			else
				HitEffects(health);

			iFrame = true;
		}
	}

	private void FixedUpdate()
	{
		iFrame = false;
	}

	public abstract void HitEffects(int health);

	public abstract void Die();
}
