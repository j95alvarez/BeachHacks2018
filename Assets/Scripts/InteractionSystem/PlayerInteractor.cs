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
            CurrentInteractable = other.GetComponent(typeof(IInteractable)) ? other.gameObject
                : CurrentInteractable; 
        }

        void OnTriggerExit(Collider other)
        {
            CurrentInteractable = other.gameObject == CurrentInteractable ? null : CurrentInteractable;
        }

        void FixedUpdate()
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(CurrentInteractable) CurrentInteractable.GetComponent<IInteractable>().Interact();
            }
        }
    }
}