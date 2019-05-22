using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class WeaponSwitch : NetworkBehaviour
{
	public GameObject[] TurretPrefabs;
    public GameObject TurretRoot;
	public GameObject AimingPoint;

	List<GameObject> spawnedTurrets = new List<GameObject>();

	GameObject currentTurret;

    [SyncVar(hook =(nameof(SwitchTo)))]
    int currentTurretIndex = 0;

    public override void OnStartClient()
    {
        SpawnTurrets();
        SwitchTo(currentTurretIndex);
    }

    private void Update()
	{
        if (!hasAuthority) return;

		if (Input.GetKeyDown("1"))
            CmdSwitchTo(0);
		else if (Input.GetKeyDown("2"))
            CmdSwitchTo(1);
		else if (Input.GetKeyDown("3"))
            CmdSwitchTo(2);
		else if (Input.GetKeyDown("4"))
            CmdSwitchTo(3);
		else if (Input.GetKeyDown("5"))
            CmdSwitchTo(4);
	}

    [Command]
    void CmdSwitchTo(int newTurretIndex)
    {
        currentTurretIndex = newTurretIndex;
    }

    [Client]
	void SwitchTo(int newTurretIndex)
	{
        if(newTurretIndex <= spawnedTurrets.Count)
        {
            GameObject newTurret = spawnedTurrets[newTurretIndex];

            currentTurret?.SetActive(false);
            newTurret.SetActive(true);

            currentTurret = newTurret;

            TurretRoot.GetComponent<DirectGunElevation>().ElevatingGun = currentTurret.transform.GetChild(1).gameObject;
        }
	}

    [Client]
    void SpawnTurrets()
    {
        foreach (var turretPrefab in TurretPrefabs)
        {
            GameObject newTurret = Instantiate(turretPrefab);

            newTurret.SetActive(false);
            newTurret.transform.SetParent(TurretRoot.transform, false);

            spawnedTurrets.Add(newTurret);

            foreach (var aiming in TurretRoot.GetComponents<Aiming>())
            {
                aiming.AimingPoint = AimingPoint;
            }
        }
    }
}
