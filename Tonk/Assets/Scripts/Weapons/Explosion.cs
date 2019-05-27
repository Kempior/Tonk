using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Weapon
{
	public int Damage = 50;
	public int Radius = 3;
	public int ForceMultiplier = 100;

	readonly int duration = 1;

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
			Health[] otherHealths = coll.transform.root.GetComponentsInChildren<Health>();
			foreach (var health in otherHealths)
			{
				if (health != null)
				{
					float damagePercent = 1 - ((transform.position - coll.transform.position).magnitude / Radius);
					damagePercent = Mathf.Clamp01(damagePercent);

					health.Hit((int)(Damage * damagePercent));
				}
			}

			// Gets only components in the object itself
			Rigidbody[] otherRbs = coll.transform.root.GetComponentsInChildren<Rigidbody>();
			foreach (var rb in otherRbs)
			{
				rb.AddExplosionForce(Damage * ForceMultiplier, transform.position, Radius);
			}
		}
	}

    public override GameObject ServerFire()
    {
        throw new System.NotImplementedException();
    }

    protected override void FireEffects()
	{
		throw new System.NotImplementedException();
	}
}
