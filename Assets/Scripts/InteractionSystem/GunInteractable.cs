using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheSquad.InteractionSystem
{
    public class GunInteractable : MonoBehaviour, IInteractable
    {
        public delegate void InteractAction();
        public event InteractAction onInteract;

        public void Interact()
        {
            //Player Picks Up Gun

            //onInteract();
        }
    }
}