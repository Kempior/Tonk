using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTimed : NetworkBehaviour
{
	public uint LifeTime = 2;

    public GameObject SpawnGameObject;

    public override void OnStartServer()
    {
        Invoke(nameof(Fire), LifeTime);
    }

    [Server]
	private void Fire()
	{
        if(SpawnGameObject != null)
        {
            GameObject newGameObject = Instantiate(SpawnGameObject, transform.position, transform.rotation);
            NetworkServer.Spawn(newGameObject);
        }

        NetworkServer.Destroy(gameObject);
	}
}
