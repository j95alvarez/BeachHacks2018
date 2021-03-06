using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

using TheSquad.Projectiles;

namespace TheSquad.Weapons
{
    public class Weapon : AbstractWeapon
    {
        [SerializeField] float spread;

        Rigidbody rb;
        float actualSpread;

        [Command]
        protected override void CmdInstantiate()
        {
            CalculateSpread();
            Debug.Log(spread);
            var randomNumberX = Random.Range(-spread, spread);
            var randomNumberY = Random.Range(-spread, spread);
            var randomNumberZ = Random.Range(-spread, spread);

            // Create the Bullet from the Bullet Prefab
            var bullet = Instantiate(
                bulletPrefab,
                bulletSpawn.position,
                bulletSpawn.rotation).gameObject;

            bullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);

            // The next concept, one that is somewhat unique to Multiplayer Networking,
            // is the concept of Network Spawning. In the Multiplayer Networking HLAPI
            // “Spawn” means more than just “Instantiate”. It means to create a GameObject
            // on the Server and on all of the Clients connected to the Server. The GameObject
            // will then be managed by the spawning system; state updates are sent to Clients 
            // when the object changes on the Server, the GameObject will be destroyed on Clients
            // when it is destroyed on the Server, and the spawned GameObject be added to the set
            // of networked GameObjects that the Server is managing so that if another Client joins
            // the game later, the objects will also be spawned on that Client in the correct state.
            NetworkServer.Spawn(bullet);

            spread = actualSpread;
        }

        protected override void Start()
        {
            base.Start();
            rb = GetComponentInParent<Rigidbody>();
            actualSpread = spread;
        }

        void CalculateSpread()
        {
            PlayerMovement move = GetComponentInParent<PlayerMovement>();
            if(move.pressDown) spread = spread/2;
        }
        
    }
}