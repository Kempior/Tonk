using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHealth : Health
{
	public float ResetTime = 5;

	private Vector3 startingPosition;
	private Quaternion startingRotation;
	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();

		startingPosition = transform.position;
		startingRotation = transform.rotation;
	}

	public override void HitEffects(int health)
	{
		Debug.Log($"I {transform.name}, {CurrentHealth}/{StartingHealth} HP (hit for {health} HP)");
	}

	public override void Die()
	{
		Debug.Log($"I {transform.name}, {CurrentHealth}/{StartingHealth} HP");
		Invoke(nameof(ResetPosition), ResetTime);
	}

	void ResetPosition()
	{
		transform.position = startingPosition;
		transform.rotation = startingRotation;

		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;

		CurrentHealth = StartingHealth;
	}
}
