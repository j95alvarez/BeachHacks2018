using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

using TheSquad.Projectiles;

namespace TheSquad.Weapons
{
    public abstract class AbstractWeapon: NetworkBehaviour, IFireable
    {
        [SerializeField] protected AbstractProjectile bulletPrefab;
        [SerializeField] protected Transform bulletSpawn;
        [SerializeField] float cooldown;

        bool readyToFire;

        protected virtual void Start()
        {
            readyToFire = true;
        }

        public void Fire()
        {
            if(bulletPrefab && readyToFire)
            {
                CmdInstantiate();
                StartCoroutine(CoStartCooldown());
            }
        }

        // The [Command] attribute indicates that the following function will be
        // called by the Client, but will be run on the Server. Any arguments in 
        // the function will automatically be passed to the Server with the 
        // Command. Commands can only be sent from the local player object. 
        // When making a networked command, the function name must begin with “Cmd”.
        protected abstract void CmdInstantiate();

        IEnumerator CoStartCooldown()
        {
            readyToFire = false;
            yield return new WaitForSeconds(cooldown);
            readyToFire = true;
        }
    }
}
