
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerController : NetworkBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    
    public override  void OnStartLocalPlayer() {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    // Update is called once per frame
    private void Update () {
        // Checks to see if it is not the local player so that only 
        // the local player processes the input
        if (!isLocalPlayer) { return; }

		var horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var vertical = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, horizontal, 0);
        transform.Translate(0, 0, vertical);

        if (Input.GetKeyDown(KeyCode.Space)) { CmdFire(); }
    }

    // The [Command] attribute indicates that the following function will be
    // called by the Client, but will be run on the Server. Any arguments in 
    // the function will automatically be passed to the Server with the 
    // Command. Commands can only be sent from the local player object. 
    // When making a networked command, the function name must begin with “Cmd”.
    [Command]
    void CmdFire() {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

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

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }

}
