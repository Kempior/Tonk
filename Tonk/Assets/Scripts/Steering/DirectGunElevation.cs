using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectGunElevation : Aiming
{
	public GameObject ElevatingGun;

	[Tooltip("In deg/s")]
	public float RotationRate = 50;
	[Tooltip("In deg")]
	public float MaxElevation = 20;
	[Tooltip("In deg")]
	public float MaxDepression = 10;

	void FixedUpdate()
	{
		// Heavily inspired by (a.k.a. copied from) https://github.com/brihernandez/GunTurrets
		// Since we're casting world to parent local coords there may be some discrepancy if the tank is on a heavily angled slope
		// This can be fixed by placing the object in an empty parent
		Vector3 localTargetPos = ElevatingGun.transform.parent.InverseTransformPoint(AimingPoint.transform.position);
		localTargetPos.x = 0;

		// Create new rotation towards the target in local space.
		Quaternion targetRot = Quaternion.LookRotation(localTargetPos);
		Quaternion currentRot = Quaternion.RotateTowards(ElevatingGun.transform.localRotation, targetRot, RotationRate * Time.deltaTime);

		float elevation = currentRot.eulerAngles.x;
		if (elevation > MaxDepression && elevation < 180)
			elevation = MaxDepression;
		else if (elevation < 360 - MaxElevation && elevation > 180)
			elevation = 360 - MaxElevation;

		ElevatingGun.transform.localRotation = Quaternion.Euler(new Vector3(elevation, 0, 0));
	}
}
