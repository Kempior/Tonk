using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
	public GameObject[] StartingTurrets;
	public GameObject TurretRoot;
	public GameObject AimingPoint;

	List<GameObject> spawnedTurrets = new List<GameObject>();
	GameObject currentTurret;

	private void Start()
	{
		foreach (var turret in StartingTurrets)
		{
			AddTurret(turret);
		}
		SwitchTo(0);
	}

	private void Update()
	{
		if (Input.GetKeyDown("1"))
			SwitchTo(0);
		else if (Input.GetKeyDown("2"))
			SwitchTo(1);
		else if (Input.GetKeyDown("3"))
			SwitchTo(2);
		else if (Input.GetKeyDown("4"))
			SwitchTo(3);
		else if (Input.GetKeyDown("5"))
			SwitchTo(4);
	}

	void SwitchTo(int newTurret)
	{
		try
		{
			GameObject newInstance = spawnedTurrets[newTurret];

			currentTurret?.SetActive(false);

			newInstance.SetActive(true);
			currentTurret = newInstance;
		}
		catch
		{
			Debug.Log("An instance of a turret wasn't instantiated yet.");
		}
	}

	void AddTurret(GameObject newTurret, bool isEnabled = false)
	{
		GameObject newInstance = Instantiate(newTurret, TurretRoot.transform);
		newInstance.SetActive(isEnabled);

		var aimings = newInstance.GetComponents<Aiming>();

		foreach (var aim in aimings)
		{
			aim.AimingPoint = AimingPoint;
		}

		spawnedTurrets.Add(newInstance);
	}
}
