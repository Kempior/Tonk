using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAiming : MonoBehaviour
{
    [HideInInspector]
	public GameObject AimingPoint;
	public float AimingDistance = 100;

	private Camera mainCamera;

	private void Start()
	{
		mainCamera = GetComponent<Camera>();
	}

	void Update()
    {
		// Cast a ray forward a set distance, set AimingPoint's position to the ray's hit point or to its end
		Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

		int layerMask = ~LayerMask.GetMask("TankHull");
		layerMask &= ~LayerMask.GetMask("TankTurret");

		if (Physics.Raycast(ray, out RaycastHit hit, AimingDistance, layerMask))
			AimingPoint.transform.position = hit.point;
		else
			AimingPoint.transform.position = ray.GetPoint(AimingDistance);
    }
}
