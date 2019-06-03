using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawns : MonoBehaviour
{
	public GameObject spawnsParent;
	private Transform[] spawns;

	public Transform Next
	{
		get
		{
			if (spawns == null)
				GetSpawns();

			return spawns[index++ % spawns.Length];
		}
	}

	private uint index = 0;

	private void GetSpawns()
	{
		List<Transform> newSpawns = new List<Transform>();

		foreach (Transform child in spawnsParent.transform)
		{
			newSpawns.Add(child);
		}

		spawns = newSpawns.ToArray();
	}
}
