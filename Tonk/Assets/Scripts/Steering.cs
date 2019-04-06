using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
	[SerializeField] private float acceleration = 1;
	[SerializeField] private float rotationSpeed = 5;
	
	private Rigidbody rb;
	private WheelCollider[] wheels;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();

		wheels = GetComponentsInChildren<WheelCollider>();
	}

	private void FixedUpdate()
	{
		foreach (var wheel in wheels)
			if(Input.GetAxis("Vertical") < 0.01 || Input.GetAxis("Horizontal") < 0.01)
				wheel.motorTorque = 0.001f;


		// Acceleration calculations
		int groundedWheels = 0;
		foreach (WheelCollider wheel in wheels)
			if (wheel.isGrounded)
				groundedWheels++;

		float traction = (float)groundedWheels / wheels.Length;

		rb.AddForce(transform.forward * traction * acceleration * Input.GetAxis("Vertical"), ForceMode.Acceleration);

		// Rotating the tank in a less ugly way
		rb.AddTorque(transform.up * rotationSpeed * Input.GetAxis("Horizontal"), ForceMode.Acceleration);

		// Slightly pulls the velocity towards forward direction
		//Vector3 targetVelocity = transform.forward * Vector3.ProjectOnPlane(rb.velocity, Vector3.up).magnitude;
		//targetVelocity.y = rb.velocity.y;
		//rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime * 3);
	}
}
