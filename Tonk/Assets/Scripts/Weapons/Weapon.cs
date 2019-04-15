using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public GameObject ProjectileSpawn;
	public GameObject SpawnedGO;

	protected abstract void Fire();
	protected abstract void FireEffects();
}
