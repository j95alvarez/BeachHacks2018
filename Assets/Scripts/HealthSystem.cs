using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine;

namespace TheSquad.HealthSystem
{
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
                currentHealth = maxHealth;

                // called on the Server, but invoked on the Clients
                RpcRespawn();
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
                // move back to zero location
                transform.position = Vector3.zero;
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
}