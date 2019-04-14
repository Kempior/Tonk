using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Weapon
{
	public int Damage = 50;
	public int Radius = 3;

	int duration = 5;

	private void Start()
	{
		Fire();

		Destroy(gameObject, duration);
	}

	protected override void Fire()
	{
		Collider[] hitObjects = Physics.OverlapSphere(transform.position, Radius);

		foreach (var coll in hitObjects)
		{
			Health otherHealth = coll.GetComponent<Health>();
			if (otherHealth != null)
			{
				float damagePercent = Radius / (transform.position - coll.transform.position).magnitude;

				otherHealth.Hit((int)(Damage * damagePercent));
			}

			Rigidbody otherRb = coll.GetComponent<Rigidbody>();
			if (otherRb != null)
			{

			}
		}
	}

	protected override void FireEffects()
	{
		throw new System.NotImplementedException();
	}
}
