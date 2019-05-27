using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTimed : Weapon
{
	public uint Time = 2;

	private void Start()
	{
		Invoke(nameof(Fire), Time);
	}

	protected override void Fire()
	{
		Instantiate(SpawnedGO, transform.position, transform.rotation);

		Destroy(this);
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
