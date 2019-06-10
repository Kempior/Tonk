using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : NetworkBehaviour
{
	public int Radius = 3;
	public int Force = 100;

    public int Damage;

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
            Health[] otherHealths = collision.transform.root.GetComponentsInChildren<Health>();
            foreach (var health in otherHealths)
            {
                float damagePercent = 1 - ((transform.position - collision.transform.position).magnitude / Radius);
                damagePercent = Mathf.Clamp01(Mathf.Clamp01(damagePercent) + 0.2f);

                health.Hit((int)(Damage * damagePercent));
            }

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
