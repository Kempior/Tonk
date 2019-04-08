using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AntiRoll : MonoBehaviour
{
	public float AntiRollForce = 35;

	private List<WheelCollider> leftWheels = new List<WheelCollider>();
	private List<WheelCollider> rightWheels = new List<WheelCollider>();

	void Start()
	{
		var wheels = GetComponentsInChildren<WheelCollider>();

		foreach (var wheel in wheels)
		{
			if (wheel.transform.localPosition.x < 0)
				leftWheels.Add(wheel);
			else
				rightWheels.Add(wheel);
		}

		// Sorts wheels in pairs, according to their z position
		leftWheels.OrderBy(wheel => wheel.transform.localPosition.z);
		rightWheels.OrderBy(wheel => wheel.transform.localPosition.z);
	}

	private void FixedUpdate()
	{
		for (int i = 0; i < 1; i++)
		{
			// Inspired by http://projects.edy.es/trac/edy_vehicle-physics/wiki/TheStabilizerBars

			WheelHit hit = new WheelHit();
			float travelL = 1.0f;
			float travelR = 1.0f;

			bool groundedL = leftWheels[i].GetGroundHit(out hit);
			if (groundedL)
				travelL = (-leftWheels[i].transform.InverseTransformPoint(hit.point).y - leftWheels[i].radius)
						  / leftWheels[i].suspensionDistance;

			bool groundedR = rightWheels[i].GetGroundHit(out hit);
			if (groundedR)
				travelR = (-rightWheels[i].transform.InverseTransformPoint(hit.point).y - rightWheels[i].radius)
						  / rightWheels[i].suspensionDistance;

			float antiRollForce = (travelL - travelR) * AntiRollForce;

			if (groundedL)
				GetComponent<Rigidbody>().AddForceAtPosition(leftWheels[i].transform.up * -antiRollForce, leftWheels[i].transform.position);
			if (groundedR)
				GetComponent<Rigidbody>().AddForceAtPosition(rightWheels[i].transform.up * antiRollForce, rightWheels[i].transform.position);
		}

	}
}
