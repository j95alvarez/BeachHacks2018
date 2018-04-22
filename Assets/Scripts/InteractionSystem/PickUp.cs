using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

using TheSquad.Weapons;
using TheSquad.AttackSystem;

namespace TheSquad.InteractionSystem
{
    public class PickUp : NetworkBehaviour, IInteractable
    {
        [SerializeField] GameObject weapon;

        public delegate void InteractAction();
        public event InteractAction onInteract;

        public void Interact(GameObject interactor)
        {
            GameObject oldWeapon = interactor.GetComponentInChildren<AbstractWeapon>().gameObject;
            weapon.transform.parent = oldWeapon.transform.parent;

            Destroy(oldWeapon);
            
            Debug.Log("Interacted");
            //onInteract();

            Destroy(gameObject);
        }
    }
}