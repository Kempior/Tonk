using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public GameObject ProjectileSpawn;
	public GameObject SpawnedGO;

    [HideInInspector]
    public bool hasAuthority = false;
    [HideInInspector]
    public ProxyWeapon proxyWeapon;

    protected abstract void Fire();
    public abstract GameObject ServerFire();

	protected abstract void FireEffects();
}
