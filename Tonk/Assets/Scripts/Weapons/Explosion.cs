using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : NetworkBehaviour
{
	public int Radius = 3;
	public int Force = 100;

	readonly int duration = 1;

    public override void OnStartServer()
    {
        Invoke(nameof(DelayedDestroy), duration);
    }

    public override void OnStartClient()
    {
        Collide();
    }

    private void Collide()
    {
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, Radius);

        foreach(var collision in hitObjects)
        {
            //collision.attachedRigidbody.AddExplosionForce(Force, transform.position, Radius);

            Rigidbody[] otherRbs = collision.transform.root.GetComponentsInChildren<Rigidbody>();
            foreach (var rb in otherRbs)
            {
                rb.AddExplosionForce(Force, transform.position, Radius);
            }
        }
    }

    private void DelayedDestroy()
    {
        NetworkServer.Destroy(gameObject);
    }
}
