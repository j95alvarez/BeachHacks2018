using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheSquad.InteractionSystem
{
    public class PlayerInteractor : MonoBehaviour
    {
        public GameObject CurrentInteractable { get; private set; }

        void OnTriggerEnter(Collider other)
        {
            // If Collider Is Of Type IInteractable, Make It The New CurrentInteractable
            CurrentInteractable = other.GetComponent(typeof(IInteractable)) ? other.gameObject
                : CurrentInteractable;
        }

        void OnTriggerExit(Collider other)
        {
            // If The Object That Leaves Is The CurrentInteractable, Set It To Null
            CurrentInteractable = other.gameObject == CurrentInteractable ? null : CurrentInteractable;
        }

        void FixedUpdate()
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                // If Interactable Exists Call Interact
                if(CurrentInteractable) CurrentInteractable.GetComponent<IInteractable>().Interact(transform.root.gameObject);
            }
        }
    }
}