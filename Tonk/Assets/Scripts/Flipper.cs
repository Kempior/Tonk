using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Flipper : MonoBehaviour
{
	[SerializeField]
	private float FlippedTime = 5;
	[SerializeField]
	private float MaxGroundedPercentage = 0.2f;
	[SerializeField]
	private float MaxAngle = 70;

	private Rigidbody rb;
	private WheelCollider[] wheels;
	private float minSinX;

	private readonly System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		wheels = GetComponentsInChildren<WheelCollider>();

		minSinX = Mathf.Sin(Mathf.Deg2Rad * (90 - MaxAngle));
	}

	void Update()
	{
		if (IsFlipped())
		{
			if (!sw.IsRunning)
			{
				sw.Restart();
			}
			else if (sw.Elapsed.TotalSeconds > FlippedTime)
			{
				Debug.Log("Gonna flip");
				Flip();
			}
		}
		else
		{
			sw.Reset();
		}
	}

	bool IsFlipped ()
	{
		return 
			transform.up.y < minSinX &&
			GroundedPercentage() < MaxGroundedPercentage
			&& rb.velocity.magnitude < 0.1;
	}

	float GroundedPercentage()
	{
		return wheels.Where(wheel => wheel.isGrounded).Count() / wheels.Length;
	}

	void Flip ()
	{
		transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
	}
}
