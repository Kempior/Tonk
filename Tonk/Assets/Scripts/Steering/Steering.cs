using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

public class Steering : NetworkBehaviour
{
	[SerializeField] private float acceleration = 1;
	[SerializeField] private float rotationSpeed = 5;

	private Rigidbody rb;
	private WheelCollider[] wheels;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();

		wheels = GetComponentsInChildren<WheelCollider>();

		CmdUnlockHandbrake();
	}

	private void FixedUpdate()
	{
		if (!hasAuthority)
			return;

		CmdPassSteering(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
	}

	[Command]
	private void CmdPassSteering(float forward, float steering)
	{
		if (!isServer) return;

		// Acceleration calculations
		int groundedWheels = wheels.Where(w => w.isGrounded).Count();
		float traction = (float)groundedWheels / wheels.Length;

		rb.AddForce(transform.forward * traction * acceleration * forward, ForceMode.Acceleration);
		rb.AddTorque(transform.up * rotationSpeed * steering, ForceMode.Acceleration);
	}

	[Command]
	void CmdUnlockHandbrake()
	{
		if (!isServer) return;

		foreach (var wheel in wheels)
			wheel.motorTorque = 0.000000001f;
	}
}
