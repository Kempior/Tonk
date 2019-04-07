using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Gun : Weapon
{
	public float ProjectileSpeed = 10;
	public float ReloadTime = 1;
	public GameObject Projectile;

	// Does nothing yet
	public bool IsFullAuto = false;

	private readonly Stopwatch stopwatch = new Stopwatch();

	void Start()
	{
		// Check if projectile has proper components on it

		stopwatch.Start();
	}

	protected override void Fire()
	{
		GameObject newProjectile = Instantiate(Projectile, BarrelEnd.transform.position, BarrelEnd.transform.rotation);
		newProjectile.GetComponent<Rigidbody>().velocity = BarrelEnd.transform.forward * ProjectileSpeed;

		stopwatch.Restart();
	}

	// Update is called once per frame
	void Update()
	{
		if (stopwatch.Elapsed.TotalSeconds > ReloadTime &&
			(Input.GetButtonDown("Fire1") ||
			(Input.GetButton("Fire1") && IsFullAuto)))
		{
			Fire();
		}
	}
}
