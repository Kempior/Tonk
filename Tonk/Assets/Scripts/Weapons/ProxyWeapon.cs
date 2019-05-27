using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyWeapon : NetworkBehaviour
{
    public Weapon weapon;

    [Command]
    public void CmdFire()
    {
        GameObject newProjectile = weapon.ServerFire();
        NetworkServer.Spawn(newProjectile);
    }
}
