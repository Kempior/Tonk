using Mirror;
using System.Linq;
using UnityEngine;

public class Player : NetworkBehaviour
{
	public GameObject tankPrefab;

	public GameObject cameraPrefab;

	private GameManager manager;
	private Spawns spawns;

	private GameObject thisTank;

	private void Start()
	{
		manager = FindObjectOfType<GameManager>();
	}

	public override void OnStartServer()
	{
		spawns = FindObjectOfType<Spawns>();
	}

	public override void OnStartLocalPlayer()
	{
		CmdSpawnTank();
	}

	[Command]
	void CmdSpawnTank()
	{
        SpawnTank();
	}

    void SpawnTank()
    {
        Transform spawnPoint = spawns.Next;
        thisTank = Instantiate(tankPrefab, spawnPoint.position, spawnPoint.rotation);

        NetworkServer.SpawnWithClientAuthority(thisTank, connectionToClient);

        Health health = thisTank.GetComponent<Health>();
        health.KilledHandler += HandleKilled;

        RpcReturnTank(thisTank);
    }

	[ClientRpc]
	void RpcReturnTank(GameObject tank)
	{
		thisTank = tank;

		if (!isLocalPlayer) return;

		GameObject camera = Instantiate(cameraPrefab, thisTank.transform.Find("CameraAnchor").transform);
        camera.GetComponentInChildren<CameraAiming>().AimingPoint = thisTank.GetComponent<AimingPointObject>().AimingPoint;
	}

	private void HandleKilled()
    {
        NetworkServer.Destroy(thisTank);
        SpawnTank();
    }
}
