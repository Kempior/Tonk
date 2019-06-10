using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileContact : NetworkBehaviour
{
    public GameObject OnHitSpawnGameObject;

    public int DirectDamage;

	private void OnCollisionEnter(Collision collision)
	{
        if (!isServer) { return; }

        Health health = collision.gameObject.GetComponentInParent<Health>();

        if(health != null)
        {
            health.Hit(DirectDamage);
        }

		if (OnHitSpawnGameObject != null)
        {
            GameObject newGameObject = Instantiate(OnHitSpawnGameObject, transform.position, transform.rotation);
            NetworkServer.Spawn(newGameObject);
        }

        NetworkServer.Destroy(gameObject);
	}
}
