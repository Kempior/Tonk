using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Weapon
{
	public int Damage = 1;

	protected override void Fire()
	{
		// Deal damage to the thing it collided with
		throw new System.NotImplementedException();
	}

	protected override void FireEffects()
	{
		throw new System.NotImplementedException();
	}

	private void OnCollisionEnter(Collision collision)
	{
		Health otherHealth = collision.transform.GetComponent<Health>();

		if (otherHealth != null)
			otherHealth.Hit(Damage);

		Destroy(gameObject);
	}
}
