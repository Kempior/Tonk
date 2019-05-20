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

    public override void OnStartAuthority()
    {
        CmdSpawnTurrets();
        CmdSwitchTo(0);
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
    void CmdSwitchTo(int newTurret)
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

        //RpcSwitchTo(newTurret);
    }

    [ClientRpc]
	void RpcSwitchTo(int newTurret)
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

    [Command]
    void CmdSpawnTurrets()
    {
        foreach (var turretPrefab in TurretPrefabs)
        {
            GameObject newTurret = Instantiate(turretPrefab);
            //newTurret.SetActive(false);

            NetworkServer.SpawnWithClientAuthority(newTurret, gameObject.GetComponent<NetworkIdentity>().clientAuthorityOwner);

            //RpcSetupTurretAimPoint(newTurret);
            
            spawnedTurrets.Add(newTurret);
        }
    }

    [ClientRpc]
	void RpcSetupTurretAimPoint(GameObject newTurret)
	{
        newTurret.SetActive(false);
        newTurret.transform.SetParent(TurretRoot.transform, false);

		var aimings = newTurret.GetComponents<Aiming>();

		foreach (var aim in aimings)
		{
			aim.AimingPoint = AimingPoint;
		}
	}
}
