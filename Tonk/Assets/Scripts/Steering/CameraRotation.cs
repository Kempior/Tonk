using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
	public float MouseSensitivity = 10;

	private GameObject cameraGO;

    // Start is called before the first frame update
    void Start()
    {
		cameraGO = GetComponentInChildren<Camera>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.visible)
        {
            return;
        }

        transform.rotation = transform.rotation * (Quaternion.Euler(0, Input.GetAxis("Mouse X") * Time.fixedDeltaTime * MouseSensitivity, 0));

		cameraGO.transform.rotation = cameraGO.transform.rotation * (Quaternion.Euler(-Input.GetAxis("Mouse Y") * Time.fixedDeltaTime * MouseSensitivity, 0, 0));
	}
}
