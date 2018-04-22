using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine;

public class HealthSystem : NetworkBehaviour {

    public int maxHealth;

    // To make our current health and damage system 
    // network aware and working under Server authority,
    // we need to use State Synchronization and a special 
    // member variable on networked objects called SyncVars.
    // Network synchronized variables, or SyncVars, are indicated
    // with the attribute [SyncVar]. 
    //
    // SyncVar hooks will link a function to the SyncVar. These 
    // functions are invoked on the Server and all Clients when the
    // value of the SyncVar changes.
    [SyncVar(hook = "OnChangeHealth")]
    private int currentHealth;

    [SerializeField]
    private RectTransform healthbar;

    [SerializeField]
    private bool destroyOnDeath;

    [SerializeField]
    private NetworkStartPosition[] spawnPoints;

    private void OnStart() {
        if (isLocalPlayer) {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }

    public int Health {
        get { return currentHealth; }
        set { currentHealth += value; }
    }

    void Start() {
        currentHealth = maxHealth;
    }

    public void Heal(int amount) {
        currentHealth += amount;
    }

    public void TakeDamage(int amount) {
        // a check for “isServer” so that damage 
        // will only be applied on the Server.
        if (!isServer) { return; }

        currentHealth -= amount;

        if (isDead()) {
            // Shooting the enemies will cause them to lose health 
            // and when their health reaches zero, the enemies will
            // be destroyed. The players, however, should retain the 
            // existing behaviour where they are moved back to the origin
            // point when they “killed”, with their health restored to maximum.
            if (destroyOnDeath) {
                Destroy(gameObject);
            } else {
                currentHealth = maxHealth;

                // called on the Server, but invoked on the Clients
                RpcRespawn();
            }
        }
    }

    // ClientRpc calls can be sent from any spawned object on the 
    // Server with a NetworkIdentity. Even though this function 
    // is called on the Server, it will be executed on the Clients.
    // ClientRpc's are the opposite of Commands. Commands are called 
    // on the Client, but executed on the Server. ClientRpc's are called
    // on the Server, but executed on the Client.
    [ClientRpc]
    private void RpcRespawn() {
        if (isLocalPlayer) {
            // Set the spawn point to origin as a default value
            Vector3 spawnPoint = Vector3.zero;

            // If there is a spawn point array and the array is not empty, pick one at random
            if (spawnPoints != null && spawnPoints.Length > 0) {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }

            // Set the player’s position to the chosen spawn point
            transform.position = spawnPoint;
        }
    }

    private void OnChangeHealth(int currentHealth) {
        healthbar.sizeDelta = new Vector2(currentHealth, healthbar.sizeDelta.y);
    }

    public bool isDead() {
        if (currentHealth <= 0) {
            return true;
        }
        return false;
    }
}