using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileContact : NetworkBehaviour
{
    public GameObject OnHitSpawnGameObject;

	private void OnCollisionEnter(Collision collision)
	{
        if (!isServer) { return; }

		if (OnHitSpawnGameObject != null)
        {
            GameObject newGameObject = Instantiate(OnHitSpawnGameObject, transform.position, transform.rotation);
            NetworkServer.Spawn(newGameObject);
        }

        NetworkServer.Destroy(gameObject);
	}
}
