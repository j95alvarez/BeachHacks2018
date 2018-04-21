
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

        if (Input.GetKeyDown(KeyCode.Space)) { Fire(); }
    }

    void Fire() {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }

}
