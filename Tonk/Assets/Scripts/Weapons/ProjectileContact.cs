using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileContact : Weapon
{
	public int Damage = 1;

	protected override void Fire()
	{
		throw new System.NotImplementedException();
	}

    public override GameObject ServerFire()
    {
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

		if(ProjectileSpawn == null)
			ProjectileSpawn = gameObject;
		if (SpawnedGO != null)
			Instantiate(SpawnedGO, ProjectileSpawn.transform.position, ProjectileSpawn.transform.rotation);

		Destroy(gameObject);
	}
}
