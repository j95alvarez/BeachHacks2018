using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour {
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private int numberofEnemies;


    // While player GameObjects are spawned when a Client connects to the Host, 
    // enemy GameObjects need to be controlled by the Server.
    //
    //The namespace UnityEngine.Networking is required.
    // 1. The class needs to derive from NetworkBehaviour.
    // 2. The class uses an implementation of the virtual function OnStartServer.
    // 3. When the server starts, a number of enemies are generated at a random 
    //      position and rotation and then these are spawned using NetworkServer.Spawn(enemy).
    //
    // OnStartServer is very similar to OnStartLocalPlayer, used earlier in this lesson to change
    // the player GameObject’s color. In this case, OnStartServer is called on the Server when
    // the Server starts listening to the Network.
    public override void OnStartServer() {
        base.OnStartServer();

        for(int i = 0; i < numberofEnemies; i++) {
            var spawnPosition = new Vector3(
                Random.Range(-8.0f, 8.0f),
                0.0f,
                Random.Range(-8.0f, 8.0f));

            var spawnRotation = Quaternion.Euler(
                0.0f,
                Random.Range(0, 180),
                0.0f);

            var enemy = (GameObject)Instantiate(enemyPrefab, spawnPosition, spawnRotation);
            NetworkServer.Spawn(enemy);
        }
    }
}