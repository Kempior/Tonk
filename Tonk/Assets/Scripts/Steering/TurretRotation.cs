using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class TurretRotation : Aiming
{
	public GameObject RotatingTurret;

	[Tooltip("In deg/s")]
	public float RotationRate = 50;

	void FixedUpdate()
	{
        // Heavily inspired by (a.k.a. copied from) https://github.com/brihernandez/GunTurrets
        // Since we're casting world to parent local coords there may be some discrepancy if the tank is on a heavily angled slope
        // This can be fixed by placing the object in an empty parent
        Vector3 localTargetPos = RotatingTurret.transform.parent.InverseTransformPoint(AimingPoint.transform.position);
		localTargetPos.y = 0.0f;

		// Create new rotation towards the target in local space.
		if (localTargetPos != Vector3.zero)
		{
			Quaternion rotationGoal = Quaternion.LookRotation(localTargetPos);
			Quaternion currentRot = Quaternion.RotateTowards(RotatingTurret.transform.localRotation, rotationGoal, RotationRate * Time.deltaTime);

			RotatingTurret.transform.localRotation = currentRot;
		}
	}
}
