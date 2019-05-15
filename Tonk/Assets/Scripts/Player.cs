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
		spawns = FindObjectOfType<Spawns>();
	}

	public override void OnStartLocalPlayer()
	{
		CmdSpawnTank();
	}

	[Command]
	void CmdSpawnTank()
	{
		thisTank = Instantiate(tankPrefab, spawns.Next);

		NetworkServer.SpawnWithClientAuthority(thisTank, gameObject);

		RpcReturnTank(thisTank.GetComponent<NetworkIdentity>().netId);
	}

	[ClientRpc]
	void RpcReturnTank(uint tankNetId)
	{
		thisTank = FindObjectsOfType<NetworkIdentity>().Where(x => x.netId == tankNetId).First().gameObject;

		if (!isLocalPlayer) return;

		Instantiate(cameraPrefab, thisTank.transform.Find("CameraAnchor").transform);
	}

	private void Update()
	{
		//if (thisTank == null)
		//	Debug.LogError("Something's wrong - we've got a null");
		//else
		//	if (!thisTank.GetComponent<Steering>().hasAuthority)
		//		Debug.LogError("No Authority");



		//if (tank.Health.IsDead)
		//{
		//	// Do ded stuff
		//}
	}
}
